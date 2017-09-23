﻿using System;
using System.Windows;
using System.Windows.Controls;
using Windows.Matic.v1.Core;
using Windows.Matic.v1.Core.Task;

namespace Windows.Matic.v1.Client.UserControls
{
    /// <summary>
    /// Interaction logic for RecordTask.xaml
    /// </summary>
    public partial class NewTask : UserControl
    {
        public Action RecordingStartedAction;
        public event EventHandler<NewTaskFinalizedEventArgs> RaiseNewTaskFinalized;

        public NewTask()
        {
            InitializeComponent();

        }

        private void Button_Click_StartRecording(object sender, RoutedEventArgs e)
        {
            btn_start_recording.IsEnabled = false;
            RecordingStartedAction?.Invoke();
            MaticCoreFacade.RecordTask(TaskRecordingCompleted);
        }

        public void TaskRecordingCompleted(InputChain ic)
        {
            UserTask task = new UserTask(txtTaskName.Text, ic);
            RaiseNewTaskFinalized?.Invoke(this, new NewTaskFinalizedEventArgs(task));
        }
    }

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
}
