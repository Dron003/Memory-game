using System;
using System.Diagnostics;
using System.Threading;

namespace memory_game {
    class Timer {
        private Stopwatch timer = new Stopwatch();
        private long seconds = 0;
        public void start() { 
            timer.Start();
            seconds = 0;
        }
        public void stop() { 
            timer.Stop(); 
            seconds = timer.ElapsedMilliseconds / 1000;
        }
        public long secondsElapsed() {
            return seconds;
        }
    }
}