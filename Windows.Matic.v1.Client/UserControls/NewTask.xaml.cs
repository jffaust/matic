﻿using System;
using System.Windows;
using System.Windows.Controls;
using Windows.Matic.v1.Recorder.Logger;
using Windows.Matic.v1.Task;

namespace Windows.Matic.v1.UserControls
{
    public class NewTaskFinalizedEventArgs : EventArgs
    {
        private UserTask _userTask;

        public NewTaskFinalizedEventArgs(UserTask userTask)
        {
            _userTask = userTask;
        }

        public UserTask UserTask
        {
            get { return _userTask; }
        }
    }

    /// <summary>
    /// Interaction logic for RecordTask.xaml
    /// </summary>
    public partial class NewTask : UserControl
    {
        public Action RecordingStartedAction;
        public event EventHandler<NewTaskFinalizedEventArgs> RaiseNewTaskFinalized;

        private InputLogger _inputLogger;

        public NewTask()
        {
            InitializeComponent();

            _inputLogger = new InputLogger();
            _inputLogger.RecordingDoneAction = FinalizeUserTask;
        }

        private void Button_Click_StartRecording(object sender, RoutedEventArgs e)
        {
            btn_start_recording.IsEnabled = false;
            RecordingStartedAction?.Invoke();
            _inputLogger.StartLogging();
        }

        public void FinalizeUserTask()
        {
            UserTask ut = new UserTask(txtTaskName.Text, _inputLogger.CurrentSession.InputChain);
            OnRaiseNewTaskFinalized(ut);
        }

        private void OnRaiseNewTaskFinalized(UserTask ut)
        {
            EventHandler<NewTaskFinalizedEventArgs> handler = RaiseNewTaskFinalized;

            if (handler != null)
            {
                NewTaskFinalizedEventArgs ea = new NewTaskFinalizedEventArgs(ut);
                handler(this, ea);
            }
        }
    }
}