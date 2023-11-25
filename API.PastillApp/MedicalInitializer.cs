using API.PastillApp.Domain.Entities;
using API.PastillApp.Repositories;

namespace API.PastillApp
{
    public class MedicalInitializer
    {
      
        public MedicalInitializer() { }

        public async static Task Initialize(PastillAppContext context)
        {
            var medicamentos = new List<Medicine>
            {
                new Medicine { Name = "Paracetamol", Dosage = 500 },
                new Medicine { Name = "Ibuprofeno", Dosage = 400 },
                new Medicine { Name = "Amoxicilina", Dosage = 500 },
                new Medicine { Name = "Omeprazol", Dosage = 20 },
                new Medicine { Name = "Loratadina", Dosage = 10 },
                new Medicine { Name = "Dipirona", Dosage = 500 },
                new Medicine { Name = "Cetirizina", Dosage = 10 },
                new Medicine { Name = "Diclofenac", Dosage = 50 },
                new Medicine { Name = "Enalapril", Dosage = 5 },
                new Medicine { Name = "Simvastatina", Dosage = 10 },
                new Medicine { Name = "Metformina", Dosage = 500 },
                new Medicine { Name = "Amlodipina", Dosage = 5 },
                new Medicine { Name = "Hidroclorotiazida", Dosage = 25 },
                new Medicine { Name = "Losartán", Dosage = 50 },
                new Medicine { Name = "Salbutamol", Dosage = 100 },
                new Medicine { Name = "Aspirina", Dosage = 100 },
                new Medicine { Name = "Levotiroxina", Dosage = 50 },
                new Medicine { Name = "Ciprofloxacina", Dosage = 750 },
                new Medicine { Name = "Clonazepam", Dosage = 2 },
                new Medicine { Name = "Sildenafil", Dosage = 50 },
                new Medicine { Name = "Pantoprazol", Dosage = 40 },
                new Medicine { Name = "Diazepam", Dosage = 5 },
                new Medicine { Name = "Lansoprazol", Dosage = 30 },
                new Medicine { Name = "Metronidazol", Dosage = 500 },
                new Medicine { Name = "Clorfenamina", Dosage = 4 },
                new Medicine { Name = "Furosemida", Dosage = 40 },
                new Medicine { Name = "Sinvastatina", Dosage = 20 },
                new Medicine { Name = "Fluoxetina", Dosage = 20 },
                new Medicine { Name = "Lorazepam", Dosage = 2 },
                new Medicine { Name = "Ranitidina", Dosage = 300 },
                new Medicine { Name = "Risperidona", Dosage = 1 },
                new Medicine { Name = "Tramadol", Dosage = 50 }
            };

            await context.Medicines.AddRangeAsync(medicamentos);
            
            await context.SaveChangesAsync();
        }
    }
}
