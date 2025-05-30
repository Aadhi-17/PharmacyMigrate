using KRATOS.DataAcessLayer;
using KRATOS.Progress;
using KRATOS.ServiceContracts;
using KRATOS.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace KRATOS.Core
{
    public class PharmacyMigrtorCore
    {
        private int Pharmacyerror = 0;
        private int PharmacyLocationerror = 0;
        private int PharmacyPatienterror = 0;
        private int PharmacyPrescriptionerror = 0;
        private int TotalTasks;
        private string stagingConnectionString = null;
        private string MySQLConnectionString = null;
        int numberOfDataPerBatch;
        double numberOfBatches;
        int offset;
        public IProgress<PharmacyProgress> PharmacyProgress { get; set; }

        public async Task InitiatePharmacyMigrationTaskAsync(PharmacyMigratorViewModel pharmacyMigratorViewModel, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var pharmacyProgress = new Progress<PharmacyProgress>();
            pharmacyProgress.ProgressChanged += PharmacyProgress_ProgressChanged;
            try
            {
                var usefulcore = new UsefullCore();
                PharmacyProgress.Report(new PharmacyProgress()
                {
                    LogCategory = LogCategory.Milestone,
                    Message = $"PharmacyMigration Starting...",
                    TotalProgress = 10
                });
                MySQLConnectionString = GetDB.MySQLConnection_String(pharmacyMigratorViewModel.Environment);
                stagingConnectionString = GetDB.Connection_String(pharmacyMigratorViewModel.ServerName, pharmacyMigratorViewModel.DatabaseName);
                pharmacyMigratorViewModel.stagingConnectionString = stagingConnectionString;
                pharmacyMigratorViewModel.MySQLConnectionString = MySQLConnectionString;
                int totalEntries = await usefulcore.GetTotalEntryToMigrateAsync(pharmacyMigratorViewModel.stagingConnectionString);

                #region Pharmacy

                cancellationToken.ThrowIfCancellationRequested();
                PharmacyCore pharmacyCore = new PharmacyCore
                {
                    PharmacyProgress = pharmacyProgress
                };
                TotalTasks = await pharmacyCore.GetPendingPharmacyToMigrateAsync(pharmacyMigratorViewModel.stagingConnectionString);
                numberOfDataPerBatch = GetNumberOfDataPerBatch();
                numberOfBatches = Math.Ceiling((double)TotalTasks / (double)numberOfDataPerBatch);
                offset = 0;
                for (double batch = 0; batch < numberOfBatches; batch++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    DataTable charts = await pharmacyCore.GetAllPendingPharmacyToMigrateAsync(pharmacyMigratorViewModel.stagingConnectionString, numberOfDataPerBatch, offset);
                    if (charts.Rows.Count == 0)
                    {
                        break;
                    }
                    List<Pharmacy> PharmacyToImportInBatch = new List<Pharmacy>();
                    foreach (DataRow pharmacy in charts.Rows)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        Pharmacy chart = pharmacyCore.GetPharmacy(await pharmacyCore.GetPharmacyInfoAsync(pharmacyMigratorViewModel.stagingConnectionString, Convert.ToInt32(pharmacy["pharmacyid"])));
                        PharmacyToImportInBatch.Add(chart);
                    }
                    await pharmacyCore.ExportPharmacyToMySQLAsync(PharmacyToImportInBatch, pharmacyMigratorViewModel.MySQLConnectionString, pharmacyMigratorViewModel.stagingConnectionString);
                    PharmacyProgress.Report(new PharmacyProgress()
                    {
                        LogCategory = LogCategory.Info,
                        Message = $"{numberOfDataPerBatch} rows inserted in to Pharmacy"
                    });
                }
                Pharmacyerror = pharmacyCore.error;
                if (Pharmacyerror == 0)
                {
                    PharmacyProgress.Report(new PharmacyProgress()
                    {
                        LogCategory = LogCategory.Milestone,
                        Message = $"Pharmacy Table Migrated",
                        TotalProgress = 30
                    });
                }
                else
                {
                    PharmacyProgress.Report(new PharmacyProgress()
                    {
                        LogCategory = LogCategory.Warn,
                        Message = $"Pharmacy Table Migrated with error",
                        TotalProgress = 30
                    });
                }

                #endregion

                #region PharmacyLocation

                cancellationToken.ThrowIfCancellationRequested();
                PharmacyLocationCore pharmacyLocationCore = new PharmacyLocationCore { PharmacyProgress = pharmacyProgress };
                TotalTasks = await pharmacyLocationCore.GetPendingPharmacyLocationToMigrateAsync(pharmacyMigratorViewModel.stagingConnectionString);
                numberOfDataPerBatch = GetNumberOfDataPerBatch();
                numberOfBatches = Math.Ceiling((double)TotalTasks / (double)numberOfDataPerBatch);
                offset = 0;
                for (double batch = 0; batch < numberOfBatches; batch++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    DataTable charts = await pharmacyLocationCore.GetAllPendingPharmacyLocationToMigrateAsync(pharmacyMigratorViewModel.stagingConnectionString, numberOfDataPerBatch, offset);
                    if (charts.Rows.Count == 0)
                    {
                        break;
                    }
                    List<PharmacyLocation> pharmacyLocationToImportInBatch = new List<PharmacyLocation>();
                    foreach (DataRow pharmacy in charts.Rows)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        PharmacyLocation chart = pharmacyLocationCore.GetPharmacyLocation(await pharmacyLocationCore.GetPharmacyLocationInfoAsync(pharmacyMigratorViewModel.stagingConnectionString, Convert.ToInt32(pharmacy["PharmacyLocationId"])));
                        pharmacyLocationToImportInBatch.Add(chart);
                    }
                    await pharmacyLocationCore.ExportPharmacyLocationsToMySQLAsync(pharmacyLocationToImportInBatch, pharmacyMigratorViewModel.MySQLConnectionString, pharmacyMigratorViewModel.stagingConnectionString);
                    PharmacyProgress.Report(new PharmacyProgress()
                    {
                        LogCategory = LogCategory.Info,
                        Message = $"{numberOfDataPerBatch} rows inserted in to PharmacyLocation"
                    });
                }
                PharmacyLocationerror = pharmacyLocationCore.error;
                if (PharmacyLocationerror == 0)
                {
                    PharmacyProgress.Report(new PharmacyProgress()
                    {
                        LogCategory = LogCategory.Milestone,
                        Message = $"PharmacyLocation Table Migrated",
                        TotalProgress = 50
                    });
                }
                else
                {
                    PharmacyProgress.Report(new PharmacyProgress()
                    {
                        LogCategory = LogCategory.Warn,
                        Message = $"PharmacyLocation Table Migrated with error",
                        TotalProgress = 50
                    });
                }

                #endregion

                #region PharmacyPatient

                cancellationToken.ThrowIfCancellationRequested();
                PharmacyPatientCore pharmacyPatient = new PharmacyPatientCore { PharmacyProgress = pharmacyProgress };
                TotalTasks = await pharmacyPatient.GetPendingPharmacyPatientToMigrateAsync(pharmacyMigratorViewModel.stagingConnectionString);
                numberOfDataPerBatch = GetNumberOfDataPerBatch();
                numberOfBatches = Math.Ceiling((double)TotalTasks / (double)numberOfDataPerBatch);
                offset = 0;
                for (double batch = 0; batch < numberOfBatches; batch++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    DataTable charts = await pharmacyPatient.GetAllPendingPharmacyPatientToMigrateAsync(pharmacyMigratorViewModel.stagingConnectionString, numberOfDataPerBatch, offset);
                    if (charts.Rows.Count == 0)
                    {
                        break;
                    }
                    List<PharmacyPatient> pharmacyPatientToImportInBatch = new List<PharmacyPatient>();
                    foreach (DataRow pharmacy in charts.Rows)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        PharmacyPatient chart = pharmacyPatient.GetPharmacyPatient(await pharmacyPatient.GetPharmacyPatientInfoAsync(pharmacyMigratorViewModel.stagingConnectionString, Convert.ToInt32(pharmacy["patientid"])));
                        pharmacyPatientToImportInBatch.Add(chart);
                    }
                    await pharmacyPatient.ExportPharmacyPatientsToMySQLAsync(pharmacyPatientToImportInBatch, pharmacyMigratorViewModel.MySQLConnectionString, pharmacyMigratorViewModel.stagingConnectionString);
                    PharmacyProgress.Report(new PharmacyProgress()
                    {
                        LogCategory = LogCategory.Info,
                        Message = $"{numberOfDataPerBatch} rows inserted in to PharmacyPatient"
                    });
                }
                PharmacyPatienterror = pharmacyPatient.error;
                if (PharmacyPatienterror == 0)
                {
                    PharmacyProgress.Report(new PharmacyProgress()
                    {
                        LogCategory = LogCategory.Milestone,
                        Message = $"PharmacyPatient Table Migrated",
                        TotalProgress = 70
                    });
                }
                else
                {
                    PharmacyProgress.Report(new PharmacyProgress()
                    {
                        LogCategory = LogCategory.Warn,
                        Message = $"PharmacyPatient Table Migrated with error",
                        TotalProgress = 70
                    });
                }

                #endregion

                #region PharmacyPrescription

                cancellationToken.ThrowIfCancellationRequested();
                PharmacyPrescriptionCore pharmacyPrescriptionCore = new PharmacyPrescriptionCore { PharmacyProgress = pharmacyProgress };
                TotalTasks = await pharmacyPrescriptionCore.GetPendingPharmacyPrescriptionToMigrateAsync(pharmacyMigratorViewModel.stagingConnectionString);
                numberOfDataPerBatch = GetNumberOfDataPerBatch();
                numberOfBatches = Math.Ceiling((double)TotalTasks / (double)numberOfDataPerBatch);
                offset = 0;
                for (double batch = 0; batch < numberOfBatches; batch++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    DataTable charts = await pharmacyPrescriptionCore.GetAllPendingPharmacyPrescriptionToMigrateAsync(pharmacyMigratorViewModel.stagingConnectionString, numberOfDataPerBatch, offset);
                    if (charts.Rows.Count == 0)
                    {
                        break;
                    }
                    List<PharmacyPrescription> pharmacyPrescriptionToImportInBatch = new List<PharmacyPrescription>();
                    foreach (DataRow pharmacy in charts.Rows)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        PharmacyPrescription chart = pharmacyPrescriptionCore.GetPharmacyPrescription(await pharmacyPrescriptionCore.GetPharmacyPrescriptionInfoAsync(pharmacyMigratorViewModel.stagingConnectionString, Convert.ToInt32(pharmacy["prescriptionid"])));
                        pharmacyPrescriptionToImportInBatch.Add(chart);
                    }
                    await pharmacyPrescriptionCore.ExportPharmacyPrescriptionsToMySQLAsync(pharmacyPrescriptionToImportInBatch, pharmacyMigratorViewModel.MySQLConnectionString, pharmacyMigratorViewModel.stagingConnectionString);
                    PharmacyProgress.Report(new PharmacyProgress()
                    {
                        LogCategory = LogCategory.Info,
                        Message = $"{numberOfDataPerBatch} rows inserted in to PharmacyPrescription"
                    });
                }
                PharmacyPrescriptionerror = pharmacyPrescriptionCore.error;
                if (PharmacyPrescriptionerror == 0)
                {
                    PharmacyProgress.Report(new PharmacyProgress()
                    {
                        LogCategory = LogCategory.Milestone,
                        Message = $"PharmacyPrescription Table Migrated",
                        TotalProgress = 90
                    });
                }
                else
                {
                    PharmacyProgress.Report(new PharmacyProgress()
                    {
                        LogCategory = LogCategory.Warn,
                        Message = $"PharmacyPrescription Table Migrated with error",
                        TotalProgress = 90
                    });
                }

                #endregion

                if (Pharmacyerror == 1 || PharmacyLocationerror == 1 || PharmacyPrescriptionerror == 1 || PharmacyPatienterror == 1)
                {
                    PharmacyProgress.Report(new PharmacyProgress()
                    {
                        LogCategory = LogCategory.Warn,
                        Message = $"PharmacyMigration is completed with error",
                        TotalProgress = 100
                    });
                }
                else
                {
                    PharmacyProgress.Report(new PharmacyProgress()
                    {
                        LogCategory = LogCategory.Success,
                        Message = $"PharmacyMigration Is Completed",
                        TotalProgress = 100
                    });
                }
            }
            catch (Exception ex)
            {
                PharmacyProgress.Report(new PharmacyProgress()
                {
                    LogCategory = LogCategory.Error,
                    Message = $"An unexpected error occurred: {ex.Message}"
                });
            }
        }

        private int GetNumberOfDataPerBatch()
        {
            return 100;
        }

        private void PharmacyProgress_ProgressChanged(object sender, PharmacyProgress e)
        {
            PharmacyProgress.Report(new PharmacyProgress()
            {
                Message = e.Message,
                TotalProgress = e.TotalProgress,
                LogCategory = e.LogCategory
            });

        }

    }
}
