using System;
using System.Threading;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameDataLibrary;

#if WINDOWS_PHONE
using System.Windows;
#endif

namespace BitSits_Framework
{
    /// <summary>
    /// All the Contents of the Game is loaded and stored here
    /// so that all other screen can copy from here
    /// </summary>
    public class GameContent
    {
        public ContentManager content;
        
        public Random random = new Random();

        public readonly float b2Scale = 30;

        // Textures
        public Texture2D blank;
        public Texture2D menuBackground, mainMenuTitle;

        // Fonts
        public SpriteFont debugFont, gameFont;
        public int gameFontSize;
        

        /// <summary>
        /// Load GameContents
        /// </summary>
        public GameContent(GameComponent screenManager)
        {
            content = screenManager.Game.Content;

            blank = content.Load<Texture2D>("Graphics/blank");
            menuBackground = content.Load<Texture2D>("Graphics/menuBackground");

            mainMenuTitle = content.Load<Texture2D>("Graphics/BitSitsGamesLogo");

            debugFont = content.Load<SpriteFont>("Fonts/debugFont");

            gameFontSize = 60;
            gameFont = content.Load<SpriteFont>("Fonts/chunky" + gameFontSize.ToString());

            MediaPlayer.IsRepeating = true;

#if DEBUG
            MediaPlayer.Volume = .4f; SoundEffect.MasterVolume = .4f;
#else
            if (BitSitsGames.Settings.MusicEnabled) PlayMusic();

            if (BitSitsGames.Settings.SoundEnabled) SoundEffect.MasterVolume = 1;
            else SoundEffect.MasterVolume = 0;
#endif

            // Initialize audio objects.
            //audioEngine = new AudioEngine("Content/Audio/Audio.xgs");
            //soundBank = new SoundBank(audioEngine, "Content/Audio/Sound Bank.xsb");
            //waveBank = new WaveBank(audioEngine, "Content/Audio/Wave Bank.xwb");
            //soundBank.GetCue("music").Play();


            //Thread.Sleep(1000);

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            screenManager.Game.ResetElapsedTime();
        }

        public void PlayMusic()
        {
            if (MediaPlayer.GameHasControl)
            {
                if (MediaPlayer.State == MediaState.Paused)
                {
                    MediaPlayer.Resume();

                    return;
                }
            }
            else if (MediaPlayer.State == MediaState.Playing)
            {
#if WINDOWS_PHONE
                MessageBoxResult Choice;

                Choice = MessageBox.Show("Media is currently playing, do you want to stop it?",
                    "Stop Player", MessageBoxButton.OKCancel);

                if (Choice == MessageBoxResult.OK) MediaPlayer.Pause();
                else
                {
                    BitSitsGames.Settings.MusicEnabled = false;
                    return;
                }
#endif
            }

            //MediaPlayer.Play(content.Load<Song>("Audio/Back to old school"));
            MediaPlayer.IsRepeating = true;
        }

        /// <summary>
        /// Unload GameContents
        /// </summary>
        public void UnloadContent() { content.Unload(); }
    }
}
