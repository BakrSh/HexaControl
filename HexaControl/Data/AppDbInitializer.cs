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
               
                if (!context.Commons.Any())
                {
                    context.Commons.AddRange(new List<CommonQues>()
                    {
                        new CommonQues()
                        {
                            Question="ما الذي احتاجه قبل بدء العمل معكم",
                            Answer="جواب للسؤال المطروح في الأعلى بشكل واضح ولكن غير مفصل بحيث يبقي بعد التساؤلات التي تضطره للتواصل معنا",
                           


                        },
                         new CommonQues()
                        {
                            Question="ما الذي احتاجه قبل بدء العمل معكم",
                            Answer="جواب للسؤال المطروح في الأعلى بشكل واضح ولكن غير مفصل بحيث يبقي بعد التساؤلات التي تضطره للتواصل معنا",



                        },
                          new CommonQues()
                        {
                            Question="ما الذي احتاجه قبل بدء العمل معكم",
                            Answer="جواب للسؤال المطروح في الأعلى بشكل واضح ولكن غير مفصل بحيث يبقي بعد التساؤلات التي تضطره للتواصل معنا",



                        },
                           new CommonQues()
                        {
                            Question="ما الذي احتاجه قبل بدء العمل معكم",
                            Answer="جواب للسؤال المطروح في الأعلى بشكل واضح ولكن غير مفصل بحيث يبقي بعد التساؤلات التي تضطره للتواصل معنا",



                        },

                    });

                    context.SaveChanges();

                }


                if (!context.Contacts.Any())
                {
                    context.Contacts.AddRange(new List<Contact>()
                    {
                        new Contact()
                        {
                            Email="hexa@hexa.com",
                            Massege="the best",
                           


                        }


                    });

                    context.SaveChanges();

                }



                if (!context.Footers.Any())
                {
                    context.Footers.AddRange(new List<Footer>()
                    {
                        new Footer()
                        {
                            FirstNum="+970 599-528022",
                            SecondNum="+970 599-528022",
                            FirstEmail="info@hexa.business",
                            SecondEmail="info@hexa.business",
                            FirstNumLocation="Gaza – Palestine",
                            SecondLocation="Istanbul – Turkey",




                        }


                    });

                    context.SaveChanges();

                }

                if (!context.Maps.Any())
                {
                    context.Maps.AddRange(new List<Map>()
                    {
                        new Map()
                        {
                            Url="https://www.google.com/maps/embed?pb=!1m14!1m12!1m3!1d45766.5555097842!2d34.45134703226789!3d31.50713854529114!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!5e0!3m2!1sen!2s!4v1676480317546!5m2!1sen!2s",
                            Title=" ",
                           

                        },

                          new Map()
                        {
                            Url="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d25027.122362031318!2d34.425298843900045!3d31.472901998607806!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x14fd823a6f9e5d1d%3A0x8b45deb1a82e1ae0!2sUniversity%20of%20Palestine!5e0!3m2!1sen!2s!4v1677435327615!5m2!1sen!2s   ",
                            Title=" ",


                        },
                    });

                    context.SaveChanges();

                }


                if (!context.Blogs.Any())
                {
                    context.Blogs.AddRange(new List<Blog>()
                    {
                        new Blog()
                        {
                            MainArticle="ما هو التصميم المتحور حول الإنسان وكيف يمكن الاستفادة منه في المشاريع الرقمية",
                           DeclarativeParagraph="فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18 فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiri خط 18",
                           FirstParg="فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18 فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiri خط 18",
                           SecondParg="فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18 فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiri خط 18",
                           isChecked=true,
                           


                        },
                        new Blog()
                        {
                            MainArticle="ما هو التصميم المتحور حول الإنسان وكيف يمكن الاستفادة منه في المشاريع الرقمية",
                           DeclarativeParagraph="فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18 فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiri خط 18",
                           FirstParg="فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18 فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiri خط 18",
                           SecondParg="فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18 فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiri خط 18",
                           isChecked=true,
                           


                        },
                        new Blog()
                        {
                            MainArticle="ما هو التصميم المتحور حول الإنسان وكيف يمكن الاستفادة منه في المشاريع الرقمية",
                           DeclarativeParagraph="فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18 فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiri خط 18",
                           FirstParg="فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18 فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiri خط 18",
                           SecondParg="فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18 فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiry خط 18فقرة مصغرة تقديمية مكونة من خمسة أسطر بخط EL Messiri خط 18",
                           isChecked=true,
                           


                        },


                    });

                    context.SaveChanges();

                }


                if (!context.Socials.Any())
                {
                    context.Socials.AddRange(new List<Social>()
                    {
                        new Social()
                        {
                            Url="www.facebook.com",
                           


                        },
                        new Social()
                        {
                            Url="www.facebook.com",
                           


                        },
                        new Social()
                        {
                            Url="www.facebook.com",
                           


                        },
                        new Social()
                        {
                            Url="www.facebook.com",
                           


                        },


                    });

                    context.SaveChanges();

                }
            }
        }
    }
}
