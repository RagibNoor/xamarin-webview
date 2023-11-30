using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using AVFoundation;
using Foundation;
using OfflineApp.Core.OfflineException.CustomException;
using OfflineApp.interfaces;
using OfflineApp.iOS.Implementations;
using Speech;

[assembly: Xamarin.Forms.Dependency(typeof(RecordAudioService))]
namespace OfflineApp.iOS.Implementations
{
    public class RecordAudioService : IRecordAudioService
    {
        AVAudioRecorder recorder;
        NSError error;
        NSUrl url;
        NSDictionary settings;
        bool isRecord = false;
        string path;
        private string finalText;
        private readonly string speechRecognitionErrorMsg = "Please enable recognition permission from device setting";
        private readonly string recordingErrorMsg = "Please enable recording permission from device setting";
        private readonly string noSpeechErrorMsg = "Can not recognize any speech";


        public RecordAudioService()
        {

        }

        public async Task<bool> Record()
        {
            if (!await InitializeRecordService())
            {
                throw (ChatBotException)Activator.CreateInstance(typeof(ChatBotException), recordingErrorMsg);
            }

            if (recorder == null || isRecord)
            {
                throw (ChatBotException)Activator.CreateInstance(typeof(ChatBotException), "Please check your internet connection!");
            }
            recorder.Record();
            isRecord = true;
            return true;
        }
        private Task<bool> InitializeRecordService()
        {
            var tsc = new TaskCompletionSource<bool>();
            var audioSession = AVAudioSession.SharedInstance();
            audioSession.RequestRecordPermission(granted =>
            {
                if (granted)
                {
                    var err = audioSession.SetCategory(AVAudioSessionCategory.PlayAndRecord);
                    if (err == null)
                    {
                        err = audioSession.SetActive(true);
                        tsc.SetResult(err == null && PrepareAudioRecording());
                    }
                    else
                    {
                        tsc.SetResult(false);
                    }

                }
                else
                {
                    tsc.SetResult(false);
                }
            });

            return tsc.Task;
        }
        private bool PrepareAudioRecording()
        {
            url = NSUrl.FromFilename(CreateOutputUrl());
            var audioSettings = new AudioSettings
            {
                SampleRate = 8000.0f,
                Format = AudioToolbox.AudioFormatType.LinearPCM,
                NumberChannels = 2,
                LinearPcmBitDepth = 16,
                LinearPcmFloat = false,
                LinearPcmBigEndian = false,

            };
            recorder = AVAudioRecorder.Create(url, audioSettings, out error);
            if (error == null)
            {
                return recorder.PrepareToRecord();
            }

            return false;
        }
        private string CreateOutputUrl()
        {
            string fileName = $"audio{DateTime.Now:yyyyMMddHHmmss}.wav";
            return path = Path.Combine(Path.GetTempPath(), fileName);
        }
        public bool StopRecord()
        {
            if (recorder != null)
            {
                recorder.Stop();
                isRecord = false;
                return true;
            }

            return false;
        }

        public byte[] ConvertStreamToByte()
        {
            byte[] bytes;
            using (var streamReader = new StreamReader(path))
            {
                using (var memorySteam = new MemoryStream())
                {
                    streamReader.BaseStream.CopyTo(memorySteam);
                    bytes = memorySteam.ToArray();
                }
            }

            File.Delete(path);

            return bytes;
        }

        public async Task<bool> StartSpeaking()
        {
            if (!await InitializeSpeechRecognizer())
            {
                throw (ChatBotException)Activator.CreateInstance(typeof(ChatBotException), speechRecognitionErrorMsg);
            }

            return await Record();
        }
        private Task<bool> InitializeSpeechRecognizer()
        {
            var tsc = new TaskCompletionSource<bool>();
            SFSpeechRecognizer.RequestAuthorization((SFSpeechRecognizerAuthorizationStatus status) =>
            {
                if (status == SFSpeechRecognizerAuthorizationStatus.Authorized)
                {
                    tsc.SetResult(true);
                }
                else
                {
                    tsc.SetResult(false);
                }
            });
            return tsc.Task;
        }
        public async Task<string> StopSpeaking()
        {
            if (StopRecord())
            {
                url = NSUrl.FromFilename(path);
                return await RecognizeFile(url);
            }
            throw Activator.CreateInstance<HttpRequestException>();

        }



        public async Task<string> RecognizeFile(NSUrl url)
        {
            var recognizer = new SFSpeechRecognizer();
            if (recognizer == null)
            {
                throw (ChatBotException)Activator.CreateInstance(typeof(ChatBotException), "Default language is not supported");
            }
            if (!recognizer.Available)
            {
                throw (ChatBotException)Activator.CreateInstance(typeof(ChatBotException), speechRecognitionErrorMsg);
            }
            var request = new SFSpeechUrlRecognitionRequest(url);
            return await GetRecognitionTask(request, recognizer);
        }

        private Task<string> GetRecognitionTask(SFSpeechUrlRecognitionRequest request, SFSpeechRecognizer recognizer)
        {
            var tsc = new TaskCompletionSource<string>();
            recognizer.GetRecognitionTask(request, (result, err) =>
            {
                if (err != null)
                {
                    tsc.SetException((ChatBotException)Activator.CreateInstance(typeof(ChatBotException), noSpeechErrorMsg));
                }
                if(result != null)
                {
                    if (result.Final)
                    {
                        finalText = result.BestTranscription.FormattedString;
                        tsc.SetResult(finalText);
                    }
                }
                
            });
            return tsc.Task;
        }

    }
}