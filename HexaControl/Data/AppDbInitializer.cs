using HexaControl.Infustructur;
using HexaControl.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HexaControl.Data
{
    public class AppDbInitializer
    {



        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<HexaConDbContext>();

                context.Database.EnsureCreated();
                if (!context.Heroes.Any())
                {
                    context.Heroes.AddRange(new List<Hero>()
                    {
                        new Hero()
                        {
                            MainText = "نقودك نحو الرفاهية",
                            SubText = "تواصل معنا لأي استفسار",
                            SubUrl = "www.hexa.com",
                           BtnUrl= "www.hexa.com"


                        }


                    });

                    context.SaveChanges();

                }

                if (!context.Services.Any())
                {
                    context.Services.AddRange(new List<Service>()
                    {
                        new Service()
                        {

                            IconText="التفكير التصميمي",
                            


                        },
                        new Service()
                        {

                            IconText="التفكير التصميمي",



                        },
                        new Service()
                        {

                            IconText="التفكير التصميمي",



                        },
                        new Service()
                        {

                            IconText="التفكير التصميمي",



                        }

                    });

                    context.SaveChanges();

                }



                if (!context.WhyHexas.Any())
                {
                    context.WhyHexas.AddRange(new List<WhyHexa>()
                    {
                        new WhyHexa()
                        {

                            SecTitle="لماذا تختار هكسا؟",
                            SubSecText="خدمات متعددة ومتكاملة في مكان واحد.. من مجرد فكرة هكسا ستعمل على خلق فرصك في سوق المنافسة",
                           


                        }


                    });

                    context.SaveChanges();

                }

                if (!context.WhyHexaElements.Any())
                {
                    context.WhyHexaElements.AddRange(new List<WhyHexaElement>()
                    {
                        new WhyHexaElement()
                        {

                            Text="الاستشارات",
                            WhyHexaId=1,


                        },
                        new WhyHexaElement()
                        {

                            Text="ادارة الاحتفالات",
                             WhyHexaId=1,


                        },
                        new WhyHexaElement()
                        {

                            Text="التصميم المتمحور حول الانسان",

                             WhyHexaId=1,

                        },
                        new WhyHexaElement()
                        {

                            Text="رقمنة المشاريع  ",

                             WhyHexaId=1,

                        },
                        new WhyHexaElement()
                        {

                            Text="التفكير التصميمي ",
                             WhyHexaId=1,


                        }


                    });

                    context.SaveChanges();

                }


                if (!context.HowWorks.Any())
                {
                    context.HowWorks.AddRange(new List<HowWork>()
                    {
                        new HowWork()
                        {

                            SecTitle="كيف نعمل ؟",



                        }


                    });

                    context.SaveChanges();

                }

                if (!context.howWeWorks.Any())
                {
                    context.howWeWorks.AddRange(new List<HowWeWorkElement>()
                    {
                        new HowWeWorkElement()
                        {

                            Text="التعاطف",
                            SubSecText="فهم الإحتياج الأساسي يساعدنا في فهم كيف بنقوم بالعملية بشكل مميز وبسيط.",
                            HowWorkId=1,
                            

                        },
                         new HowWeWorkElement()
                        {

                            Text="تصميم الحل",
                             SubSecText="بعد الوصول للحل الأنسب نبدأ بتصميمه والتأكد من فعالية الحل ",

                               HowWorkId=1,

                        },
                         new HowWeWorkElement()
                        {

                            Text="التغذية الراجعة",

                             SubSecText="نراقب العملية دائماً لملاحظة أين هي الأخطاء التي وقعت لإصلاحها وتجنبها مرةً أخرى",
                              HowWorkId=1,
                        },
                         new HowWeWorkElement()
                        {

                            Text="تحديد المشكلة",

                             SubSecText="يعتبر أكثر من نصف الحل, ويساهم بشكل كبير في إيجاد أفضل حلول دائمأً وأنسبها.",
                              HowWorkId=1,
                        },
                         new HowWeWorkElement()
                        {

                            Text="التنفيذ",
                             SubSecText="بعد مراقبة فعالية الحل والتأكد منها نبدأ في عملة تحويل هذا الحل لواقع ",
                              HowWorkId=1,

                        }


                    });

                    context.SaveChanges();

                }


                if (!context.Banars.Any())
                {
                    context.Banars.AddRange(new List<Banar>()
                    {
                        new Banar()
                        {

                            Title=" دعنا نبدأ رحلتنا سوياً",
                            Prgraph="فقرة ترويجية مكونة من 6 أسطر بخط El Messiri بخط بحجم 18 بكسل حجم مربع النص 350 بكس",



                        }


                    });

                    context.SaveChanges();

                }
            }
        }
    }
}
