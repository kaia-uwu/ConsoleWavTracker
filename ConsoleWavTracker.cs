using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Media;
using System.Text;

namespace Program
{
    class ConsoleWavTracker
    {
        public struct Tone
        {
            public byte[] Keys = new byte[1] { 0 };
            public double Duriation = 0;
            #region Tone(...)
            public Tone()
            {

            }
            public Tone(byte Key)
            {
                Keys[0] = Key;
            }
            public Tone(double Duriation)
            {
                this.Duriation = Duriation;
            }
            public Tone(byte Key, double Duriation)
            {
                Keys[0] = Key;
                this.Duriation = Duriation;
            }
            public Tone(byte[] Keys)
            {
                this.Keys = Keys;
            }
            public Tone(byte[] Keys, double Duriation)
            {
                this.Keys = Keys;
                this.Duriation = Duriation;
            }
            #endregion
            public void SetKeyCount(uint Count)
            {
                byte[] temp = Keys;
                Keys = new byte[Count];
                for (int i = 0; i < Count && i < temp.Length; i++)
                {
                    Keys[i] = temp[i];
                }
            }
            public bool IsWait()
            {
                if (Keys.Length == 1 && Keys[0] == 0)
                {
                    return true;
                }
                return false;
            }
            new public string ToString()
            {
                string str = "[ Keys : { ";
                for (int i = 0; i < Keys.Length; i++)
                {
                    str += Keys[i].ToString();
                    if (i < Keys.Length - 1)
                    {
                        str += ",";
                    }
                    else
                    {
                        str += " ";
                    }
                }
                str += "} | Duriation : " + Duriation + "ms | IsWait : " + IsWait() + "]";
                return str;
            }
        }
        public class Track
        {
            public Tone[] Tones = new Tone[0];
            public Track()
            {
                for (uint i = 0; i < Tones.Length; i++)
                {
                    Tones[i] = new Tone();
                }
            }
            public Track(Tone[] tones)
            {
                this.Tones = tones;
            }
            public double GetDuriation()
            {
                double duriation = 0;
                foreach (Tone tone in Tones)
                {
                    duriation += tone.Duriation;
                }
                return duriation;
            }
            new public string ToString()
            {
                string str = "<TRACK STR START>\n";
                foreach (Tone tone in Tones)
                {
                    str += tone.ToString() + "\n";
                }
                str += "<TRACK STR END>";
                return str;
            }
        }
        public class TrackWriter
        {
            //public uint BPM;
            public Track Track;
            public uint DefaultDuriation = 100;
            private uint position;
            public TrackWriter(Track Track)
            {
                this.Track = Track;
            }
            #region Add/AddAt TODO 
            public void Add(byte Key)
            {
                grow();
                Track.Tones[position] = new Tone(Key, DefaultDuriation);
                position++;
            }
            public void Add(byte Key, double Duriation)
            {
                grow();
                Track.Tones[position] = new Tone(Key, Duriation);
                position++;
            }
            public void Add(byte[] Keys)
            {
                grow();
                Track.Tones[position] = new Tone(Keys, DefaultDuriation);
                position++;
            }
            public void Add(byte[] Keys, double Duriation)
            {
                grow();
                Track.Tones[position] = new Tone(Keys, Duriation);
                position++;
            }
            public void AddAt(byte Key, uint Position)
            {
                grow(Position);
                Track.Tones[Position] = new Tone(Key, DefaultDuriation);
                if (Position > position)
                {
                    position = Position;
                }
            }
            public void AddAt(byte Key, uint Duriation, uint Position)
            {
                grow(Position);
                Track.Tones[Position] = new Tone(Key, Duriation);
                if (Position > position)
                {
                    position = Position;
                }
            }
            public void AddAt(byte[] Keys, uint Position)
            {
                grow(Position);
                Track.Tones[Position] = new Tone(Keys, DefaultDuriation);
                if (Position > position)
                {
                    position = Position;
                }
            }
            public void AddAt(byte[] Keys, uint Duriation, uint Position)
            {
                grow(Position);
                Track.Tones[Position] = new Tone(Keys, Duriation);
                if (Position > position)
                {
                    position = Position;
                }
            }
            public void AddAt(Tone Tone, uint Position)
            {
                grow(Position);
                Track.Tones[Position] = Tone;
            }
            #endregion
            #region Remove/RemoveAt TODO
            public void Remove()
            {
                Track.Tones[position] = new Tone(0, DefaultDuriation);
                position--;
            }
            public void RemoveAt(uint Position)
            {
                if (Position < Track.Tones.Length)
                {
                    Track.Tones[Position] = new Tone();
                }
            }
            #endregion
            #region Replace/... TODO
            /*
            public void ReplaceFrom(Tone Search, Tone Replacement)
            {

            }
            public void ReplaceTo(Tone Search, Tone Replacement)
            {

            }
            public void Replace(Tone Search, Tone Replacement)
            {

            }
            public void Replace(byte Search, byte Replacement)
            {

            }
            public void Replace(byte[] Search, byte[] Replacement)
            {

            }
            public void Replace(uint Search, uint Replacement)
            {

            }
            */
            #endregion
            #region Move/... TODO
            public void MoveFrom(uint From, uint Position)
            {

            }
            public void MoveTo(uint To, uint Position)
            {

            }
            public void MoveRange(uint From, uint To, uint Position)
            {

            }
            #endregion
            #region Copy/... TODO
            #endregion
            #region support functions TODO
            #region -> fill
            private void fill()
            {
                for (uint i = 0; i < Track.Tones.Length; i++)
                {
                    Track.Tones[i] = new Tone();
                }
            }
            private void fillFrom(uint start)
            {
                for (uint i = start; i < Track.Tones.Length; i++)
                {
                    Track.Tones[i] = new Tone();
                }
            }
            private void fillTo(uint end)
            {
                for (uint i = 0; i < Math.Clamp(end, 0, Track.Tones.Length); i++)
                {
                    Track.Tones[i] = new Tone();
                }
            }
            private void fillRange(uint start, uint end)
            {
                for (uint i = start; i < Math.Clamp(end, 0, Track.Tones.Length); i++)
                {
                    Track.Tones[i] = new Tone();
                }
            }
            #endregion
            #region -> grow
            private void grow()
            {
                if (Track.Tones.Length <= position)
                {
                    SetToneCount(position + 1);
                }
            }
            private void grow(uint index)
            {
                if (Track.Tones.Length <= index)
                {
                    SetToneCount(index + 1);
                }
            }
            #endregion
            #endregion
            public void SetToneCount(uint Count)
            {
                Tone[] temp = Track.Tones;
                Track.Tones = new Tone[Count];
                for (int i = 0; i < Count && i < temp.Length; i++)
                {
                    Track.Tones[i] = temp[i];
                }
                if (Track.Tones.Length > temp.Length)
                {
                    fillFrom((uint)temp.Length);
                }
            }
        }
        public class Piece
        {
            public List<Track> Tracks = new List<Track>();
            //public uint BPM;
            public Piece()
            {

            }
            public Piece(string Path)
            {
                StreamReader sr = File.OpenText(Path);
                string? buffer = sr.ReadLine();
                if (buffer == null)
                    throw new NullReferenceException();
                bool inTrack = false;
                string[] data;
                string[] keysData;
                byte[] keys;
                double dur;
                Track curTrack = default;
                TrackWriter tw = default;
                while (buffer != "end")
                {
                    if (inTrack)
                    {
                        switch (buffer)
                        {
                            case "endtrk":
                                Tracks.Add(curTrack);
                                inTrack = false;
                                break;
                            case "" or "bar" or "empty" or "spacer":
                                break;
                            default:
                                data = buffer.Split(' ');
                                keysData = data[0].Split(',');
                                keys = new byte[keysData.Length];
                                for (int i = 0; i < keysData.Length; i++)
                                {
                                    keys[i] = Convert.ToByte(keysData[i]);
                                }
                                dur = Convert.ToDouble(data[1]);
                                tw.Add(keys, dur);
                                break;
                        }
                    }
                    else
                    {
                        curTrack = new Track();
                        tw = new TrackWriter(curTrack);
                        inTrack = true;
                    }
                    buffer = sr.ReadLine();
                }
                sr.Close();
            }
            public MemoryStream GenerateWav()
            {
                #region hzLUT
                int[] hzLUT = new int[89];
                hzLUT[0] = 0;
                for (int i = 1; i <= 88; i++)
                {
                    hzLUT[i] = Math.Clamp((int)Math.Round(Math.Pow(Math.Pow(2, 1.0 / 12), i - 49) * 440), 0, 32767); ;
                }
                #endregion

                const double TAU = 2 * Math.PI;

                MemoryStream wave = new MemoryStream();
                BinaryWriter sw = new BinaryWriter(wave);

                int formatChunkSize = 16;
                int headerSize = 8;
                short formatType = 1;
                short tracks = 1;
                int samplesPerSecond = 44100;
                short bitsPerSample = 16;
                short frameSize = (short)(tracks * ((bitsPerSample + 7) / 8));
                int bytesPerSecond = samplesPerSecond * frameSize;
                int waveSize = 4;
                int samples;
                {
                    int longestTrack = 0;
                    if (Tracks.Count > 1)
                    {
                        for (int i = 1; i < Tracks.Count; i++)
                        {
                            if (Tracks[i].GetDuriation() > Tracks[longestTrack].GetDuriation())
                            {
                                longestTrack = i;
                            }
                        }
                    }
                    samples = (int)(samplesPerSecond * Tracks[longestTrack].GetDuriation() / 1000);
                }

                int dataChunkSize = samples * frameSize;
                int fileSize = waveSize + headerSize + formatChunkSize + headerSize + dataChunkSize;

                sw.Write(Encoding.UTF8.GetBytes("RIFF"));
                sw.Write(fileSize); 
                sw.Write(Encoding.UTF8.GetBytes("WAVE"));
                sw.Write(Encoding.UTF8.GetBytes("fmt "));
                sw.Write(formatChunkSize);
                sw.Write(formatType);
                sw.Write(tracks);
                sw.Write(samplesPerSecond);
                sw.Write(bytesPerSecond);
                sw.Write(frameSize);
                sw.Write(bitsPerSample);
                sw.Write(Encoding.UTF8.GetBytes("data"));
                sw.Write(dataChunkSize);

                short[] data = new short[dataChunkSize];
                double offset;

                short[] curToneData;
                int curOffset;

                foreach (Track curTrack in Tracks)
                {
                    offset = 0;

                    foreach (Tone curTone in curTrack.Tones)
                    {
                        curOffset = (int)Math.Round(offset);
                        double samplesToWrite = samplesPerSecond * curTone.Duriation / 1000f;

                        if (!curTone.IsWait())
                        {
                            curToneData = new short[(int)samplesToWrite + 1];

                            foreach (byte curKey in curTone.Keys)
                            {
                                double theta = hzLUT[curKey] * TAU / samplesPerSecond;
                                double amp = 15000 / 2;
                                double ampStep = amp / ((int)samplesToWrite / 2);

                                for (int step = 0; step < samplesToWrite; step++)
                                {
                                    short s = 0;
                                    // sine
                                    //s = (short)(amp * Math.Sin(theta * step));

                                    // square
                                    //s = (short)(amp * (Math.Sin(theta * step) > 0f ? 1f : -1f));

                                    // saw
                                    s = (short)(amp * ((((theta * step) % (2 * Math.PI)) / (2 * Math.PI)) * 2 - 1));

                                    curToneData[step] = (short)((s + curToneData[step]) / 2);

                                    if (step > (int)samplesToWrite / 2)
                                    {
                                        amp -= ampStep;
                                    }
                                }
                            }
                            for (int step = 0; step < curToneData.Length; step++)
                            {
                                data[step + curOffset] = (short)(data[step + curOffset] + curToneData[step]);
                            }
                        }
                        offset += samplesToWrite;
                    }
                }
                foreach (short s in data)
                {
                    sw.Write(s);
                }
                wave.Seek(0, SeekOrigin.Begin);
                if (File.Exists("file.wav"))
                {
                    File.Delete("file.wav");
                }
                FileStream file = File.Open("file.wav", FileMode.CreateNew);
                file.Write(wave.ToArray());
                wave.Seek(0, SeekOrigin.Begin);
                return wave;
            }
            public void Play()
            {
                if (OperatingSystem.IsWindows())
                {
                    SoundPlayer sp = new SoundPlayer(GenerateWav());
                    sp.PlaySync();
                }
            }
        }
        static void Main()
        {
            /*
             * Full      - 4b   - hollow
             * Half      - 2b   - hollow with stalk
             * Quarter   - 1b   - full
             * Eight     - 1/2b - full 1 flag
             * Sixteenth - 1/4b - full 2 flag
             * 32nd      - 1/8b - full 3 flag
             * ...
             * 
             * for 120 bpm
             * f 2000
             * h 1000
             * q 500
             * e 250
             * s 125
             * t 62
            */

            Piece piece = new Piece("Megalovania.score");
            //Piece piece = new Piece("data.score");

            piece.Play();
        }
    }
}
