using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Point1
{
    class Sounder
    {
        // www.mediacollege.com/downloads/sound-effects/explosion/
        public SoundEffect se1;
        public SoundEffectInstance se1_instance1;
        public Song song1;
        


        public void Sing()
        {
            MediaPlayer.Play(song1);
        }

        public void Crash()
        {
            float volume = 1.0f;
            float pitch = 0.0f;
            float pan = 0.0f;
            se1.Play(volume, pitch, pan);
            //se1.Play();
            //se1_instance1.Play();
            
        }

        public void InitSounder()
        {
            se1 =  Game1.Instance.Content.Load<SoundEffect>("explosion-01");
            song1 = Game1.Instance.Content.Load<Song>("explosion-02");
            SoundEffect.MasterVolume = 1f;
            se1_instance1 = se1.CreateInstance();


        }
    }
}
