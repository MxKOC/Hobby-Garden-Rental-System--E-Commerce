using FieldRent.Entity;
using Microsoft.EntityFrameworkCore;

namespace FieldRent.Data.Concrete.EfCore
{
    public static class SeedData
    {
        public static void TestVerileriniDoldur(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();

            if (context != null)
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }



                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User { UserName = "muhammed", Name = "Muhammed Koç", Email = "info@muhammed.com", Password = "123456", IsAdmin=true },
                        new User { UserName = "said", Name = "Said Darıcı", Email = "info@said.com", Password = "123456", IsAdmin=false },
                        new User { UserName = "Cem", Name = "Cem", Email = "info@cem.com", Password = "123456", IsAdmin=false },
                        new User { UserName = "Erhan", Name = "Erhan", Email = "info@erhan.com", Password = "123456", IsAdmin=false }
                    );
                    context.SaveChanges();
                }



                if (!context.Fields.Any())
                {
                    context.Fields.AddRange(
                        new Field
                        {
                            FieldCoordinate = "FirstArea",
                        },
                        new Field
                        {
                            FieldCoordinate = "SecondArea",
                        }
                        );

                    context.SaveChanges();
                }


                if (!context.Maps.Any())
                {
                    context.Maps.AddRange(
                        new Map
                        {
                            MapCoordinate = "a1(Firstarea)",
                            MapPrice = 100,
                            MapCondition = "Boş",
                            MapIsActive = true,
                            FieldId = 1
                        },
                        new Map
                        {
                            MapCoordinate = "b2(Firstarea)",
                            MapPrice = 200,
                            MapCondition = "Nadas",
                            MapIsActive = true,
                            FieldId = 1
                        },
                        new Map
                        {
                            MapCoordinate = "c3(Firstarea)",
                            MapPrice = 300,
                            MapCondition = "Nadas",
                            MapIsActive = true,
                            FieldId = 1
                        },
                        new Map
                        {
                            MapCoordinate = "c4(Secondarea)",
                            MapPrice = 400,
                            MapCondition = "Nadas",
                            MapIsActive = true,
                            FieldId = 2
                        },
                        new Map
                        {
                            MapCoordinate = "c5(Secondarea)",
                            MapPrice = 500,
                            MapCondition = "Nadas",
                            MapIsActive = true,
                            FieldId = 2
                        }



                    );
                    context.SaveChanges();
                }


                if (!context.Requests.Any())
                {
                    context.Requests.AddRange(
                        new Request
                        {
                            RequestName = "Gubre",
                            RequestPrice = 1,


                        },

                        new Request
                        {
                            RequestName = "BitkiCosturan",
                            RequestPrice = 2,


                        },
                        new Request
                        {
                            RequestName = "GunlukSulama",
                            RequestPrice = 3,


                        },
                        new Request
                        {
                            RequestName = "Isıklandırma",
                            RequestPrice = 4,


                        },
                        new Request
                        {
                            RequestName = "Kamera",
                            RequestPrice = 5,


                        },
                        new Request
                        {
                            RequestName = "Hasat",
                            RequestPrice = 6,


                        },
                        new Request
                        {
                            RequestName = "Depo",
                            RequestPrice = 7,


                        }
                        
                    );
                    context.SaveChanges();
                }







            }
        }
    }
}