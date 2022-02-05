using System;
using System.Diagnostics;
using System.Threading;

namespace memory_game {
    class Timer {
        private Stopwatch timer = new Stopwatch();
        public Timer() {
            timer.Start();
        }
        
        public void start() { timer.Start(); }
        public void stop() { timer.Stop(); }
        public long secondsElapsed() {
            timer.Stop();
            return timer.ElapsedMilliseconds / 1000;
        }
    }
}