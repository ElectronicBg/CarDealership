using CarDealership.Models;
using CarDealership.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System;

namespace CarDealership.Data
{
    public class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            //Seed Roles
            var userManager = service.GetService<UserManager<ApplicationUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();

            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));

            // creating admin

            var user = new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                Name = "Emil",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            var userInDb = await userManager.FindByEmailAsync(user.Email);
            if (userInDb == null)
            {
                await userManager.CreateAsync(user, "Admin@123");
                await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            }
        }
        public async Task SeedBrands(ApplicationDbContext context)
        {
            // Check if the Brands table is already populated
            if (context.Brands.Any())
            {
                return; // Database has been seeded
            }

            var brands = new List<Brand>
            {
                new Brand { Name = "Toyota" },
                new Brand { Name = "Honda" },
                new Brand { Name = "Ford" },
                new Brand { Name = "Chevrolet" },
                new Brand { Name = "Volkswagen" },
                new Brand { Name = "BMW" },
                new Brand { Name = "Mercedes-Benz" },
                new Brand { Name = "Audi" },
                new Brand { Name = "Nissan" },
                new Brand { Name = "Hyundai" },
            };

            // Assigning BrandIds
            /* for (int i = 0; i < brands.Count; i++)
             {
                 brands[i].BrandId = i + 1;
             }*/

            await context.Brands.AddRangeAsync(brands);
            await context.SaveChangesAsync();
        }

        public async Task SeedModels(ApplicationDbContext context)
        {
            // Check if the Models table is already populated
            if (context.Models.Any())
            {
                return; // Database has been seeded
            }

            // Create models for each brand
            var models = new List<Model>
        {
            // Models for Brand with BrandId = 1 (e.g., Toyota)
            new Model { BrandId = 1, Name = "Corolla" },
            new Model { BrandId = 1, Name = "Camry" },
            new Model { BrandId = 1, Name = "Rav4" },

            // Models for Brand with BrandId = 2 (e.g., Honda)
            new Model { BrandId = 2, Name = "Civic" },
            new Model { BrandId = 2, Name = "Accord" },
            new Model { BrandId = 2, Name = "CR-V" },

            new Model { BrandId = 3, Name = "F-150" },
            new Model { BrandId = 3, Name = "Escape" },
            new Model { BrandId = 3, Name = "Focus" },
            new Model { BrandId = 3, Name = "Explorer" },
            new Model { BrandId = 3, Name = "Mustang" },

            // Models for Chevrolet (BrandId = 6)
            new Model { BrandId = 4, Name = "Camaro" },
            new Model { BrandId = 4, Name = "Silverado" },
            new Model { BrandId = 4, Name = "Equinox" },
            new Model { BrandId = 4, Name = "Tahoe" },
            new Model { BrandId = 4, Name = "Malibu" },

            // Models for Volkswagen (BrandId = 7)
            new Model { BrandId = 5, Name = "Golf" },
            new Model { BrandId = 5, Name = "Passat" },
            new Model { BrandId = 5, Name = "Jetta" },
            new Model { BrandId = 5, Name = "Tiguan" },
            new Model { BrandId = 5, Name = "Atlas" },

            // Models for BMW (BrandId = 8)
            new Model { BrandId = 6, Name = "3 Series" },
            new Model { BrandId = 6, Name = "5 Series" },
            new Model { BrandId = 6, Name = "X3" },
            new Model { BrandId = 6, Name = "X5" },
            new Model { BrandId = 6, Name = "7 Series" },

            // Models for Mercedes-Benz (BrandId = 9)
            new Model { BrandId = 7, Name = "C-Class" },
            new Model { BrandId = 7, Name = "E-Class" },
            new Model { BrandId = 7, Name = "S-Class" },
            new Model { BrandId = 7, Name = "GLC" },
            new Model { BrandId = 7, Name = "GLE" },

            // Models for Audi (BrandId = 10)
            new Model { BrandId = 8, Name = "A3" },
            new Model { BrandId = 8, Name = "A4" },
            new Model { BrandId = 8, Name = "Q5" },
            new Model { BrandId = 8, Name = "Q7" },
            new Model { BrandId = 8, Name = "TT" },

            // Models for Nissan (BrandId = 11)
            new Model { BrandId = 9, Name = "Altima" },
            new Model { BrandId = 9, Name = "Sentra" },
            new Model { BrandId = 9, Name = "Rogue" },
            new Model { BrandId = 9, Name = "Pathfinder" },
            new Model { BrandId = 9, Name = "Maxima" },

            // Models for Hyundai (BrandId = 12)
            new Model { BrandId = 10, Name = "Elantra" },
            new Model { BrandId = 10, Name = "Sonata" },
            new Model { BrandId = 10, Name = "Tucson" },
            new Model { BrandId = 10, Name = "Santa Fe" },
            new Model { BrandId = 10, Name = "Kona" },
        };

            // Add models to the database
            await context.Models.AddRangeAsync(models);
            await context.SaveChangesAsync();
        }
        public async Task SeedCarColors(ApplicationDbContext context)
        {
            // Check if the CarColors table is already populated
            if (context.CarColors.Any())
            {
                return; // Database has been seeded
            }

            // Create car colors
            var carColors = new List<CarColor>
        {
            new CarColor { Name = "Черен", Value = "#000000" },
            new CarColor { Name = "Бял", Value = "#FFFFFF" },
            new CarColor { Name = "Червен", Value = "#FF0000" },
            new CarColor { Name = "Син", Value = "#0000FF" },
            new CarColor { Name = "Сив", Value = "#0000FF" },
            new CarColor { Name = "Зелен", Value = "#008000" },
            new CarColor { Name = "Жълт", Value = "#FFFF00" },
        };

            // Add car colors to the database
            await context.CarColors.AddRangeAsync(carColors);
            await context.SaveChangesAsync();
        }
        public async Task SeedCars(ApplicationDbContext context)
        {
            // Check if the Cars table is already populated
            if (context.Cars.Any())
            {
                return; // Database has been seeded
            }

            // Get some example data for seeding (you may fetch this data from elsewhere or create it manually)
            var brands = context.Brands.ToList();
            var models = context.Models.ToList();
            var carColors = context.CarColors.ToList();

            // Create some sample cars
            var cars = new List<Car>
            {
            new Car
            {
                BrandId = brands.FirstOrDefault(b => b.Name == "Toyota").BrandId,
                ModelId = models.FirstOrDefault(m => m.Name == "Corolla").ModelId,
                EngineType = EngineType.Petrol,
                TransmissionType = TransmissionType.Automatic,
                CarColorId = carColors.FirstOrDefault(c => c.Name == "Черен").CarColorId,
                Region = Region.Shumen,
                Year = 2020,
                Mileage = 20000,
                Power = 150,
                CarType = CarType.Sedan,
                Condition = Condition.Used,
                Price = 20000,
                Photos=new List<Photo>()
            },
            new Car
            {
                BrandId = brands.FirstOrDefault(b => b.Name == "Honda").BrandId,
                ModelId = models.FirstOrDefault(m => m.Name == "Civic").ModelId,
                EngineType = EngineType.Petrol,
                TransmissionType = TransmissionType.Manual,
                CarColorId = carColors.FirstOrDefault(c => c.Name == "Бял").CarColorId,
                Region = Region.Plovdiv,
                Year = 2018,
                Mileage = 30000,
                Power = 140,
                CarType = CarType.Sedan,
                Condition = Condition.Used,
                Price = 18000,
                Photos=new List<Photo>()
            },
            new Car
            {
                BrandId = brands.FirstOrDefault(b => b.Name == "Ford").BrandId,
                ModelId = models.FirstOrDefault(m => m.Name == "F-150").ModelId,
                EngineType = EngineType.Diesel,
                TransmissionType = TransmissionType.Automatic,
                CarColorId = carColors.FirstOrDefault(c => c.Name == "Син").CarColorId,
                Region = Region.Sofia,
                Year = 2019,
                Mileage = 25000,
                Power = 250,
                CarType = CarType.SUV,
                Condition = Condition.Used,
                Price = 35000,
                Photos=new List<Photo>()
            },
            new Car
            {
                BrandId = brands.FirstOrDefault(b => b.Name == "Chevrolet").BrandId,
                ModelId = models.FirstOrDefault(m => m.Name == "Camaro").ModelId,
                EngineType = EngineType.Petrol,
                TransmissionType = TransmissionType.Manual,
                CarColorId = carColors.FirstOrDefault(c => c.Name == "Червен").CarColorId,
                Region = Region.Varna,
                Year = 2021,
                Mileage = 10000,
                Power = 300,
                CarType = CarType.Sedan,
                Condition = Condition.New,
                Price = 45000,
                Photos=new List<Photo>()
            },
            new Car
            {
                BrandId = brands.FirstOrDefault(b => b.Name == "Volkswagen").BrandId,
                ModelId = models.FirstOrDefault(m => m.Name == "Golf").ModelId,
                EngineType = EngineType.Petrol,
                TransmissionType = TransmissionType.Automatic,
                CarColorId = carColors.FirstOrDefault(c => c.Name == "Черен").CarColorId,
                Region = Region.Burgas,
                Year = 2017,
                Mileage = 40000,
                Power = 120,
                CarType = CarType.Hatchback,
                Condition = Condition.Used,
                Price = 15000,
                Photos=new List<Photo>()
            },
            new Car
            {
                BrandId = brands.FirstOrDefault(b => b.Name == "BMW").BrandId,
                ModelId = models.FirstOrDefault(m => m.Name == "3 Series").ModelId,
                EngineType = EngineType.Petrol,
                TransmissionType = TransmissionType.Automatic,
                CarColorId = carColors.FirstOrDefault(c => c.Name == "Сив").CarColorId,
                Region = Region.Pleven,
                Year = 2020,
                Mileage = 20000,
                Power = 180,
                CarType = CarType.Sedan,
                Condition = Condition.Used,
                Price = 28000,
                Photos=new List<Photo>()
            },
            // Car 7
            new Car
            {
                BrandId = brands.FirstOrDefault(b => b.Name == "Audi").BrandId,
                ModelId = models.FirstOrDefault(m => m.Name == "A3").ModelId,
                EngineType = EngineType.Petrol,
                TransmissionType = TransmissionType.Automatic,
                CarColorId = carColors.FirstOrDefault(c => c.Name == "Черен").CarColorId,
                Region = Region.Sofia,
                Year = 2019,
                Mileage = 22000,
                Power = 150,
                CarType = CarType.Sedan,
                Condition = Condition.Used,
                Price = 24000,
                Photos=new List<Photo>()
            },
            // Car 8
            new Car
            {
                BrandId = brands.FirstOrDefault(b => b.Name == "Nissan").BrandId,
                ModelId = models.FirstOrDefault(m => m.Name == "Altima").ModelId,
                EngineType = EngineType.Petrol,
                TransmissionType = TransmissionType.Automatic,
                CarColorId = carColors.FirstOrDefault(c => c.Name == "Бял").CarColorId,
                Region = Region.Varna,
                Year = 2016,
                Mileage = 35000,
                Power = 170,
                CarType = CarType.Sedan,
                Condition = Condition.Used,
                Price = 19000,
                Photos=new List<Photo>()
            },
            // Car 9
            new Car
            {
                BrandId = brands.FirstOrDefault(b => b.Name == "Hyundai").BrandId,
                ModelId = models.FirstOrDefault(m => m.Name == "Elantra").ModelId,
                EngineType = EngineType.Petrol,
                TransmissionType = TransmissionType.Automatic,
                CarColorId = carColors.FirstOrDefault(c => c.Name == "Сив").CarColorId,
                Region = Region.Burgas,
                Year = 2018,
                Mileage = 28000,
                Power = 140,
                CarType = CarType.Sedan,
                Condition = Condition.Used,
                Price = 17000,
                Photos=new List<Photo>()
            },
            // Car 10
            new Car
            {
                BrandId = brands.FirstOrDefault(b => b.Name == "Mercedes-Benz").BrandId,
                ModelId = models.FirstOrDefault(m => m.Name == "C-Class").ModelId,
                EngineType = EngineType.Diesel,
                TransmissionType = TransmissionType.Automatic,
                CarColorId = carColors.FirstOrDefault(c => c.Name == "Черен").CarColorId,
                Region = Region.Sofia,
                Year = 2020,
                Mileage = 18000,
                Power = 200,
                CarType = CarType.Sedan,
                Condition = Condition.Used,
                Price = 32000,
                Photos=new List<Photo>()
            },
            // Car 11
            new Car
            {
                BrandId = brands.FirstOrDefault(b => b.Name == "Toyota").BrandId,
                ModelId = models.FirstOrDefault(m => m.Name == "Rav4").ModelId,
                EngineType = EngineType.Hybrid,
                TransmissionType = TransmissionType.Automatic,
                CarColorId = carColors.FirstOrDefault(c => c.Name == "Син").CarColorId,
                Region = Region.Plovdiv,
                Year = 2020,
                Mileage = 25000,
                Power = 180,
                CarType = CarType.SUV,
                Condition = Condition.Used,
                Price = 28000,
                Photos=new List<Photo>()
            }
        };

            // Add cars to the database
            await context.Cars.AddRangeAsync(cars);
            await context.SaveChangesAsync();
        }
        public async Task SeedPhotos(ApplicationDbContext context)
        {
            // Check if the Photos table is already populated
            if (context.Photos.Any())
            {
                return; // Database has been seeded
            }

            // Get some example data for seeding (you may fetch this data from elsewhere or create it manually)
            var cars = context.Cars.ToList();

            // Create some sample photos for cars
            var photos = new List<Photo>
        {
            // Photos for Car 1
            new Photo { CarId = 1, Url = "https://cdn.motor1.com/images/mgl/YKEew/s1/2020-toyota-corolla-sedan.jpg" },
             new Photo { CarId = 1, Url = "https://cdn.dlron.us/static/dealer-12152/20Toyota-Corolla-Jellybean-XSE-BlackSand.jpeg" },
            // Photos for Car 2
            new Photo { CarId = 2, Url = "https://hips.hearstapps.com/hmg-prod/amv-prod-cad-assets/wp-content/uploads/2017/03/2017-Honda-Civic-Hatchback-Sport-Touring-119.jpg" },
             new Photo { CarId = 2, Url = "https://casmiami.com/Files/Products/163579_1.jpg" },
            // Photos for Car 3
            new Photo { CarId = 3, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQWxUaatchSaXU2GBitQZPKRT4LncoeknrynAhaI3wUcg&s" },
            // Photos for Car 4
            new Photo { CarId = 4, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS6oJjcxUEbJjBXp5aXirzgvrKWs3ngQWUsFeyVvyalug&s" },
            // Photos for Car 5
            new Photo { CarId = 5, Url = "https://i.pinimg.com/736x/f5/ba/2d/f5ba2dce1cccacce6fff20c5d9c7ecf0.jpg" },
            // Photos for Car 6
            new Photo { CarId = 6, Url = "https://www.carscoops.com/wp-content/uploads/2019/06/ef34007d-2019-bmw-330i-m-sport.jpg" },
            // Photos for Car 7
            new Photo { CarId = 7, Url = "https://assets.nexuspointapex.co.uk/resize/1024/tenant_3fd34c9d-07c2-4832-9600-eb2699b8208d/media/234846/audi-a3-30-tdi-116-black-edition-5dr-s-tronic-km19ebd-64a00dd74f505.jpg" },
            // Photos for Car 8
            new Photo { CarId = 8, Url = "https://stuwrightdotcom.files.wordpress.com/2016/05/20160526_101434.jpg" },
            // Photos for Car 9
            new Photo { CarId = 9, Url = "https://autobidcanada.com/wp-content/uploads/2021/03/Untitled-3.png" },
            // Photos for Car 10
            new Photo { CarId = 10, Url = "https://getrentacar.com/storage/cache/images/960-640-100-fit-230834.jpeg" },
            // Photos for Car 11
            new Photo { CarId = 11, Url = "https://carsguide-res.cloudinary.com/image/upload/f_auto,fl_lossy,q_auto,t_cg_hero_large/v1/editorial/2020-Toyota-RAV4-GXL-Hybrid-2WD-SUV-blue-Tung-Nguyen-1001x565p-%281%29.jpg" },
        };

            // Add photos to the database
            await context.Photos.AddRangeAsync(photos);
            await context.SaveChangesAsync();
        }
    }
}
