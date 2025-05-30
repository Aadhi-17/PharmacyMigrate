using KRATOS.Progress;
using KRATOS.ServiceContracts;
using KRATOS.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KRATOS.UserControls
{
    public partial class KRATOS : UserControl
    {
        public KRATOS()
        {
            InitializeComponent();
        }
        private List<PharmacyProgress> logs = new List<PharmacyProgress>();
        public PharmacyManager pharmacyManager = new PharmacyManager();
        public string server;
        public string dbName = "";
        private void btnStagingDatabase_Click(object sender, EventArgs e)
        {
            server = cmbxServer.SelectedItem.ToString();
            DBSelectDialog frm = new DBSelectDialog(server);
            frm.ShowDialog();
            dbName = frm.dbName;
            if (dbName != "")
            {
                lblSelectedDatabase.Text = dbName;
            }
        }

        private async void btnUpload_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;
            DialogResult result = MessageBox.Show(
                "Are you sure you want to continue?",
                "Confirmation Dialog",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                prgbxPharmacy.Value = 0;
                lblProgressPercentage.Text = "0%";
                rctxtErrorLog.Text = "";
                logs.Clear();
                EnableDisableControls(false);

                try
                {
                    btnCancel.Text = "Cancel";
                    btnCancel.Enabled = true;
                    var viewModel = MapUIControlsToViewModel();
                    pharmacyManager.PharmacyProgress = new Progress<PharmacyProgress>
                        ((status) =>
                        {
                            AppendLog(status.Message, status.LogCategory);
                            if (status.TotalProgress >= 0)
                            {
                                prgbxPharmacy.Value = (int)status.TotalProgress;
                                lblProgressPercentage.Text = status.TotalProgress + " %";
                            }
                        });
                    await pharmacyManager.InitiatePharmacyMigration(viewModel);
                    btnCancel.Text = "Reset";
                }
                catch (OperationCanceledException ocx)
                {
                    btnCancel.Text = "Reset";
                    if (ocx.CancellationToken.IsCancellationRequested)
                    {
                        MessageBox.Show("Execution cancelled by user.", "Abort", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        AppendLog("Execution cancelled by user.", LogCategory.Error);
                    }
                }
                catch (Exception ex)
                {
                    AppendLog(ex.Message, LogCategory.Error);
                    btnCancel.Text = "Reset";
                }
                finally
                {
                    btnCancel.Enabled = true;
                    EnableDisableControls(true);
                }
            }
            else
            {
                return;
            }
        }

        private PharmacyMigratorViewModel MapUIControlsToViewModel()
        {
            PharmacyMigratorViewModel viewModel = new PharmacyMigratorViewModel
            {
                ServerName = cmbxServer.SelectedItem.ToString(),
                DatabaseName = lblSelectedDatabase.Text,
                Environment = cmbxDestEnvrnment.SelectedItem.ToString()
            };
            return viewModel;
        }

        private void AppendLog(string message, LogCategory category)
        {
            logs.Add(new PharmacyProgress() { LogCategory = category, Message = message });
            RenderLog(message, category);
        }

        private void RenderLog(string message, LogCategory category)
        {
            rctxtErrorLog.SelectionStart = rctxtErrorLog.TextLength;
            rctxtErrorLog.SelectionLength = 0;
            if (category == LogCategory.Error)
            {
                rctxtErrorLog.SelectionColor = Color.Red;
            }
            else if (category == LogCategory.Success)
            {
                rctxtErrorLog.SelectionColor = Color.Green;
            }
            else if (category == LogCategory.Info)
            {
                rctxtErrorLog.SelectionColor = Color.Black;
            }
            else if (category == LogCategory.Warn)
            {
                rctxtErrorLog.SelectionColor = Color.Orange;
            }
            else if (category == LogCategory.Milestone)
            {
                rctxtErrorLog.SelectionColor = Color.Blue;
            }

            if (!string.IsNullOrEmpty(message))
            {
                rctxtErrorLog.AppendText(message);
                rctxtErrorLog.AppendText(Environment.NewLine);
                rctxtErrorLog.SelectionColor = rctxtErrorLog.ForeColor;

                rctxtErrorLog.SelectionStart = rctxtErrorLog.Text.Length;
                rctxtErrorLog.ScrollToCaret();
            }
        }

        private void chkShowErrors_CheckedChanged(object sender, EventArgs e)
        {
            RefreshLogs();
        }
        private void RefreshLogs()
        {
            rctxtErrorLog.Text = "";
            var stagerLogs = logs;

            if (chkShowErrors.Checked)
                stagerLogs = logs.Where(x => x.LogCategory == LogCategory.Error || x.LogCategory == LogCategory.Warn).ToList();

            foreach (PharmacyProgress log in stagerLogs)
            {
                RenderLog(log.Message, log.LogCategory);
            }
        }

        private async Task ResetAllFields()
        {
            lblSelectedDatabase.Text = string.Empty;
            cmbxServer.SelectedIndex = -1;
            cmbxDestEnvrnment.SelectedIndex = -1;
            rctxtErrorLog.Text = string.Empty;
            lblProgressPercentage.Text = "0%";
            prgbxPharmacy.Value = 0;
        }

        private void EnableDisableControls(bool enabled)
        {
            lblSelectedDatabase.Enabled = enabled;
            cmbxServer.Enabled = enabled;
            cmbxDestEnvrnment.Enabled = enabled;
            btnUpload.Enabled = enabled;
            btnStagingDatabase.Enabled = enabled;
        }

        private async void btnCancel_Click(object sender, EventArgs e)
        {
            if ((sender as Button).Text.Equals("Reset"))
            {
                DialogResult dialog = MessageBox.Show("Do you want to reset", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialog == DialogResult.Yes)
                {
                    await ResetAllFields();
                }
            }

            if (btnUpload.Enabled)
                return;

            DialogResult confirmation = MessageBox.Show("Are you sure you want to cancel?", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmation == DialogResult.Yes)
            {
                if (btnUpload.Enabled) return;
                pharmacyManager.CancelTask();
                btnCancel.Enabled = false;
                prgbxPharmacy.Value = 0;
            }
        }

        private bool ValidateInputs()
        {
            if (cmbxServer.SelectedIndex == -1)
            {
                MessageBox.Show("Fill all mandatory fields to begin." + "\n\nSerever is mandatory.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(lblSelectedDatabase.Text))
            {
                MessageBox.Show("Fill all mandatory fields to begin." + "\n\nDatabase is mandatory.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cmbxDestEnvrnment.SelectedIndex == -1)
            {
                MessageBox.Show("Fill all mandatory fields to begin." + "\n\nEnvrnment is mandatory.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
