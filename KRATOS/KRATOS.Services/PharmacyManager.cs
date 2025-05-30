using KRATOS.Core;
using KRATOS.Progress;
using KRATOS.ServiceContracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KRATOS.Services
{
    public class PharmacyManager
    {
        public IProgress<PharmacyProgress> PharmacyProgress { get; set; }
        private CancellationTokenSource cancellationTokenSource;

        public PharmacyManager() { cancellationTokenSource = new CancellationTokenSource(); }
        public async Task InitiatePharmacyMigration(PharmacyMigratorViewModel pharmacyMigratorViewModel)
        {
            var pharmacyProgress = new Progress<PharmacyProgress>();
            pharmacyProgress.ProgressChanged += DBCleanerServiceProgress_ProgressChanged;
            PharmacyMigrtorCore pharmacyMigrtorCore = new PharmacyMigrtorCore { PharmacyProgress = pharmacyProgress };
            await pharmacyMigrtorCore.InitiatePharmacyMigrationTaskAsync(pharmacyMigratorViewModel, cancellationTokenSource.Token);
        }

        private void DBCleanerServiceProgress_ProgressChanged(object sender, PharmacyProgress e)
        {
            PharmacyProgress.Report(new PharmacyProgress()
            {
                Message = e.Message,
                TotalProgress = e.TotalProgress,
                LogCategory = e.LogCategory
            });

        }

        public void CancelTask()
        {
            cancellationTokenSource.Cancel();
        }
    }
}
