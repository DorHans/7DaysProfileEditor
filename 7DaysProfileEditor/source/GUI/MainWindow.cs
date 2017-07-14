﻿using System;
using System.IO;
using System.Reflection;
using System.Drawing;
using System.Windows.Forms;


namespace SevenDaysProfileEditor.GUI {

    /// <summary>
    /// Main window of the program
    /// </summary>
    internal class MainWindow : Form {
        public Label focusDummy;
        public MainMenuStrip mainMenu;
        public BottomStatusBar statusBar;
        public PlayerTabControl tabs;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MainWindow() {

            Size = new Size(1000, 850);

            Text = "7 Days Profile Editor - v" + Assembly.GetEntryAssembly().GetName().Version;
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            tabs = new PlayerTabControl(this);
            statusBar = new BottomStatusBar();
            mainMenu = new MainMenuStrip(this, tabs, statusBar);

            focusDummy = new Label();
            focusDummy.Size = new Size(0, 0);
            Controls.Add(focusDummy);

            Controls.Add(tabs);
            Controls.Add(mainMenu);
            Controls.Add(statusBar);

           
        }
    }
}