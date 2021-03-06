﻿using Infragistics.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PersistenceSample
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        // 保存対象のコントロールグループ
        private PersistenceGroup taregetGroup = new PersistenceGroup();
        // 保存内容のMemoryStream
        private MemoryStream saveMemoryStrem;


        public MainWindow()
        {
            InitializeComponent();

            // 保存対象のプロパティやコントロールの設定などを行う。
            SettingSaveTargetControls();
        }

        private void BtnSaveClick(object sender, RoutedEventArgs e)
        {
            saveMemoryStrem = PersistenceManager.Save(taregetGroup);
            MessageBox.Show("保存しました。");
        }

        private void BtnLoadClick(object sender, RoutedEventArgs e)
        {
            saveMemoryStrem.Position = 0;
            if (saveMemoryStrem != null)
            {
                PersistenceManager.Load(taregetGroup, saveMemoryStrem);
                MessageBox.Show("読み込みました。");
            }
        }

        private void SettingSaveTargetControls()
        {
            // 保存対象のコントロールに対し、保存するプロパティを設定する。
            this.SetSettingsControl(this.comboBox);
            this.SetSettingsControl(this.textBox);
            this.SetSettingsControl(this.datePicker);
            this.SetSettingsControl(this.radioOnAttached);
            this.SetSettingsControl(this.radioNonAttached);
            this.SetSettingsControl(this.checkBox);
            this.SetSettingsControl(this.listView);

            // 保存対象のコントロールを１つのグループとして登録。
            PersistenceManager.SetPersistenceGroup(this.comboBox, taregetGroup);
            PersistenceManager.SetPersistenceGroup(this.textBox, taregetGroup);
            PersistenceManager.SetPersistenceGroup(this.datePicker, taregetGroup);
            PersistenceManager.SetPersistenceGroup(this.radioOnAttached, taregetGroup);
            PersistenceManager.SetPersistenceGroup(this.radioNonAttached, taregetGroup);
            PersistenceManager.SetPersistenceGroup(this.checkBox, taregetGroup);
            PersistenceManager.SetPersistenceGroup(this.listView, taregetGroup);

        }

        private void SetSettingsControl(DependencyObject target)
        {
            PersistenceSettings setting = new PersistenceSettings();
            setting.SavePersistenceOptions = Infragistics.Persistence.Primitives.PersistenceOption.OnlySpecified;

            // コントロールにより保存対象のプロパティを決定する。
            if (target is ComboBox)
            {
                setting.PropertySettings.Add(new PropertyNamePersistenceInfo() { PropertyName = "SelectedIndex" });
            }
            else if (target is TextBox)
            {
                setting.PropertySettings.Add(new PropertyNamePersistenceInfo() { PropertyName = "Text" });
            }
            else if (target is DatePicker)
            {
                setting.PropertySettings.Add(new PropertyNamePersistenceInfo() { PropertyName = "SelectedDate" });
            }
            else if (target is RadioButton)
            {
                setting.PropertySettings.Add(new PropertyNamePersistenceInfo() { PropertyName = "IsChecked" });
            }
            else if (target is CheckBox)
            {
                setting.PropertySettings.Add(new PropertyNamePersistenceInfo() { PropertyName = "IsChecked" });
            }
            else if (target is ListView)
            {
                setting.PropertySettings.Add(new PropertyNamePersistenceInfo() { PropertyName = "SelectedIndex" });
            }
            PersistenceManager.SetSettings(target, setting);
        }

        private void BtnClearClick(object sender, RoutedEventArgs e)
        {
            this.comboBox.SelectedIndex = -1;
            this.textBox.Text = null;
            this.datePicker.SelectedDate = null;
            this.radioOnAttached.IsChecked = true;
            this.radioNonAttached.IsChecked = true;
            this.checkBox.IsChecked = false;
            this.listView.SelectedIndex = -1;
        }
    }
}
