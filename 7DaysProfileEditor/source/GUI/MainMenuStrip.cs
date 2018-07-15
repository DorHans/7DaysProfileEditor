﻿using System;
using System.Windows.Forms;

namespace SevenDaysProfileEditor.GUI {

    /// <summary>
    /// The top menu strip
    /// </summary>
    internal class MainMenuStrip : MenuStrip {
        public MainMenuActions mainMenuActions;

        private ToolStripMenuItem itemAbout;
        private ToolStripMenuItem itemClose;
        private ToolStripMenuItem itemCloseAll;
        private ToolStripMenuItem itemExit;
        private ToolStripMenuItem itemOpen;
        private ToolStripMenuItem itemReload;
        private ToolStripMenuItem itemSave;
        private ToolStripMenuItem itemSaveAs;
        private ToolStripMenuItem itemSendReport;
        private PlayerTabControl playerTabs;

        /// <summary>
        /// Default contructor.
        /// </summary>
        /// <param name="mainWindow">Main window of the program</param>
        /// <param name="tabsControlPlayer">Tab control that holds all the ttps</param>
        /// <param name="statusBar">Bottom status bar</param>
        public MainMenuStrip(MainWindow mainWindow, PlayerTabControl tabsControlPlayer, BottomStatusBar statusBar) {
            this.playerTabs = tabsControlPlayer;

            mainMenuActions = new MainMenuActions(mainWindow, tabsControlPlayer, statusBar);

            ToolStripMenuItem fileMenu = new ToolStripMenuItem("File");

            itemOpen = new ToolStripMenuItem("Open...");
            itemOpen.ShortcutKeys = Keys.Control | Keys.O;
            itemOpen.Click += new EventHandler(OpenClick);
            fileMenu.DropDownItems.Add(itemOpen);

            itemReload = new ToolStripMenuItem("Reload");
            itemReload.ShortcutKeys = Keys.Control | Keys.R;
            itemReload.Click += new EventHandler(ReloadClick);
            itemReload.Enabled = false;
            fileMenu.DropDownItems.Add(itemReload);

            itemSave = new ToolStripMenuItem("Save");
            itemSave.Click += new EventHandler(SaveClick);
            itemSave.ShortcutKeys = Keys.Control | Keys.S;
            itemSave.Enabled = false;
            fileMenu.DropDownItems.Add(itemSave);

            itemSaveAs = new ToolStripMenuItem("Save As...");
            itemSaveAs.Click += new EventHandler(SaveAsClick);
            itemSaveAs.ShortcutKeys = Keys.Control | Keys.Alt | Keys.S;
            itemSaveAs.Enabled = false;
            fileMenu.DropDownItems.Add(itemSaveAs);

            fileMenu.DropDownItems.Add(new ToolStripSeparator());

            itemClose = new ToolStripMenuItem("Close");
            itemClose.Click += new EventHandler(CloseClick);
            itemClose.ShortcutKeys = Keys.Control | Keys.W;
            itemClose.Enabled = false;
            fileMenu.DropDownItems.Add(itemClose);

            itemCloseAll = new ToolStripMenuItem("Close all");
            itemCloseAll.Click += new EventHandler(CloseAllClick);
            itemCloseAll.ShortcutKeys = Keys.Control | Keys.Alt | Keys.W;
            itemCloseAll.Enabled = false;
            fileMenu.DropDownItems.Add(itemCloseAll);

            fileMenu.DropDownItems.Add(new ToolStripSeparator());

            itemExit = new ToolStripMenuItem("Exit");
            itemExit.Click += new EventHandler(ExitClick);
            itemExit.ShortcutKeys = Keys.Alt | Keys.F4;
            fileMenu.DropDownItems.Add(itemExit);

            Items.Add(fileMenu);

            ToolStripMenuItem helpMenu = new ToolStripMenuItem("Help");

            itemSendReport = new ToolStripMenuItem("Send error report");
            itemSendReport.Click += new EventHandler(sendReportClick);
            helpMenu.DropDownItems.Add(itemSendReport);

            itemAbout = new ToolStripMenuItem("About");
            itemAbout.Click += new EventHandler(AboutClick);
            helpMenu.DropDownItems.Add(itemAbout);

            Items.Add(helpMenu);
        }

        /// <summary>
        /// Triggered when file is opened/closed. Used to enable/disable aproppriate menus.
        /// </summary>
        /// <param name="tabs">Number of opened tabs</param>
        public void UpdateMenus(int tabs) {
            if (tabs > 0) {
                itemReload.Enabled = true;
                itemSave.Enabled = true;
                itemSaveAs.Enabled = true;
                itemClose.Enabled = true;
                itemCloseAll.Enabled = true;
            }
            else {
                itemReload.Enabled = false;
                itemSave.Enabled = false;
                itemSaveAs.Enabled = false;
                itemClose.Enabled = false;
                itemCloseAll.Enabled = false;
            }
        }

        /// <summary>
        /// Event handler for about button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutClick(object sender, EventArgs e) {
            new AboutWindow();
        }

        /// <summary>
        /// Event handler for close all button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseAllClick(object sender, EventArgs e) {
            foreach (PlayerTab tab in playerTabs.TabPages) {
                mainMenuActions.Close(tab);
            }
        }

        /// <summary>
        /// Event handler for close button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseClick(object sender, EventArgs e) {
            if (playerTabs.GetTabCount() > 0) {
                mainMenuActions.Close(playerTabs.GetSelectedTab());
            }
        }

        /// <summary>
        /// Event handler for exit button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitClick(object sender, EventArgs e) {
            CloseAllClick(sender, e);

            if (playerTabs.TabCount == 0) {
                Application.Exit();
            }
        }

        /// <summary>
        /// Event handler for open button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenClick(object sender, EventArgs e) {
            mainMenuActions.Open();
        }

        /// <summary>
        /// Event handler for reload button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReloadClick(object sender, EventArgs e) {
            mainMenuActions.Reload(playerTabs.GetSelectedTab());
        }

        /// <summary>
        /// Event handler for save as button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAsClick(object sender, EventArgs e) {
            if (playerTabs.GetTabCount() > 0) {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "7 Days to Die save file(*.ttp)|*.ttp";

                saveFile.FileOk += (sender1, e1) => {
                    mainMenuActions.Save(playerTabs.GetSelectedTab(), saveFile.FileName);
                };

                saveFile.ShowDialog();
            }
        }

        /// <summary>
        /// Event handler for save button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveClick(object sender, EventArgs e) {
            if (playerTabs.GetTabCount() > 0) {
                mainMenuActions.Save(playerTabs.GetSelectedTab());
            }
        }

        /// <summary>
        /// Event handler for save error report button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendReportClick(object sender, EventArgs e) {

            string path = "";

            if (playerTabs.GetTabCount() > 0) {
                path = playerTabs.GetSelectedTab().path;
            }

            ErrorHandler.SaveReport(path);
        }
    }
}