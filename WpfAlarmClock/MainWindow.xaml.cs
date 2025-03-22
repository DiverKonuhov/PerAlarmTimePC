using System;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace WpfAlarmClock
{
    public partial class MainWindow : Window
    {
        private DateTime _alarmTime;
        private bool _isAlarmSet;

        public MainWindow()
        {
            InitializeComponent();
            StartClock();
        }

        // Обновление текущего времени
        private void StartClock()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            CurrentTimeText.Text = DateTime.Now.ToString("HH:mm:ss");

            // Проверка срабатывания будильника
            if (_isAlarmSet && DateTime.Now >= _alarmTime)
            {
                _isAlarmSet = false;
                AlarmStatusText.Text = "Будильник сработал!";
                PlayAlarmSound();
            }
        }

        // Установка будильника
        private void SetAlarmButton_Click(object sender, RoutedEventArgs e)
        {
            if (DateTime.TryParse(AlarmTimeInput.Text, out _alarmTime))
            {
                _isAlarmSet = true;
                AlarmStatusText.Text = $"Будильник установлен на {_alarmTime:HH:mm}";
            }
            else
            {
                MessageBox.Show("Введите время в формате HH:mm");
            }
        }

        // Воспроизведение звука будильника
        private void PlayAlarmSound()
        {
            Task.Run(() =>
            {
                SoundPlayer player = new SoundPlayer("echoes-in-the-breeze-20240607-033742.wav"); // Убедись, что файл alarm.wav есть в проекте
                player.PlayLooping();
            });
        }
    }
}