using Entities;
using Entities.Model;
using Entities.Model.LocalModels;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CustomInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            Users user = new Users()
            {
                UserDisplayName = "Administrator",
                UserName = "administrator",
                UserPassword = "654321",
                IsActive = true,
                EditDate = 1
            };
            context.Users.Add(user);
            context.SaveChanges();


            List<UserRoles> userRole = new List<UserRoles>();
            userRole.Add(new UserRoles() { RoleName = "Admin" });
            userRole.Add(new UserRoles() { RoleName = "Kordinator" });
            userRole.Add(new UserRoles() { RoleName = "Vizitor" });
            userRole.Add(new UserRoles() { RoleName = "Satici" });
            userRole.Add(new UserRoles() { RoleName = "Dizayner" });
            context.UserRoles.AddRange(userRole);
            context.SaveChanges();

            UserRoles userRoleId = context.UserRoles.FirstOrDefault(x => x.RoleName == "Admin");
            Users userId = context.Users.FirstOrDefault(x => x.UserName == "administrator");

            UserRolesMapping roleMapping = new UserRolesMapping()
            {
                RoleID = userRoleId.ID,
                UserID = userId.UserID
            };

            context.UserRolesMappings.Add(roleMapping);

            AdditionalCharges charges = new AdditionalCharges() { charge_name = "Custom", charge_value = 0 };
            context.AdditionalCharges.Add(charges);
            context.SaveChanges();

            List<LocationGroup> locationGroup = new List<LocationGroup>();
            locationGroup.Add(new LocationGroup { Value = "Rayon və Qəsəbələr" });
            locationGroup.Add(new LocationGroup { Value = "Metrostansiyalar" });
            locationGroup.Add(new LocationGroup { Value = "Nişangahlar" });
            context.LocationGroup.AddRange(locationGroup);
            context.SaveChanges();

            List<LocationSubGroup> locationSubGroups = new List<LocationSubGroup>();
            List<LocationGroup> getLocationGroup = context.LocationGroup.ToList();
            foreach (var item in getLocationGroup)
            {
                if (item.Value == "Rayon və Qəsəbələr")
                {
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "Abşeron r." });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "Binəqədi r." });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "Xətai r." });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "Xəzər r." });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "Qaradağ r." });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "Nərimanov r." });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "Nəsimi r." });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "Nizami r." });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "Pirallahı r." });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "Sabunçu r." });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "Səbail r." });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "Suraxanı r." });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "Yasamal r." });
                }
                else if (item.Value == "Metrostansiyalar")
                {
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "2" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "A" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "B" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "D" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "E" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "Ə" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "G" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "H" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "X" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "İ" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "K" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "Q" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "M" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "N" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "S" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "Ş" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "U" });
                }
                else if (item.Value == "Nişangahlar")
                {
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "A" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "B" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "C" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "D" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "F" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "H" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "X" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "İ" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "K" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "Q" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "M" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "N" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "P" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "R" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "S" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "Ş" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "T" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "U" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "Y" });
                    locationSubGroups.Add(new LocationSubGroup() { GroupID = item.ID, Value = "Z" });
                }
            }
            context.LocationSubGroup.AddRange(locationSubGroups);
            context.SaveChanges();

            List<LocationNames> locationNames = new List<LocationNames>();
            foreach (var group in getLocationGroup)
            {
                if (group.Value == "Rayon və Qəsəbələr")
                {
                    List<LocationSubGroup> getLocationSubGroup = context.LocationSubGroup.Where(x => x.GroupID == group.ID).ToList();
                    foreach (var subgroup in getLocationSubGroup)
                    {
                        if (subgroup.Value == "Abşeron r.")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Ceyranbatan" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Çiçək" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Digah" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Fatmayı" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Görədil" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Hökməli" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Köhnə Corat" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Qobu" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Masazır" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Mehdiabad" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Müşviqabad" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Novxanı" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Pirəkəşkül" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Saray" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Yeni Corat" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Zağulba" });
                        }
                        else if (subgroup.Value == "Binəqədi r.")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "2-ci Alatava" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "28 May" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "6-cı mikrorayon" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "7-ci mikrorayon" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "8-ci mikrorayon" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "9-cu mikrorayon" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Biləcəri" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Binəqədi" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Xocəsən" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Xutor" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "M.Ə.Rəsulzadə" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Sulutəpə" });

                        }
                        else if (subgroup.Value == "Xətai r.")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Əhmədli" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Həzi Aslanov" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Köhnə Günəşli" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "NZS" });

                        }
                        else if (subgroup.Value == "Xətai r.")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Binə" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Buzovna" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Dübəndi" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Gürgən" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Qala" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Mərdəkan" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Şağan" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Şimal DRES" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Şüvəlan" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Türkan" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Zirə" });

                        }
                        else if (subgroup.Value == "Qaradağ r.")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Bibi Heybət" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Ələt" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Qızıldaş" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Qobustan" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Lökbatan" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Puta" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Sahil" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Səngəçal" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Şıxov" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Şubani" });
                        }
                        else if (subgroup.Value == "Nərimanov r.")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Böyükşor" });
                        }
                        else if (subgroup.Value == "Nəsimi r.")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "1-ci mikrorayon" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "2-ci mikrorayon" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "3-cü mikrorayon" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "4-cü mikrorayon" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "5-ci mikrorayon" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Kubinka" });
                        }
                        else if (subgroup.Value == "Nizami r.")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "8-ci kilometr" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Keşlə" });
                        }
                        else if (subgroup.Value == "Sabunçu r.")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Bakıxanov" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Balaxanı" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Bilgəh" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Kürdəxanı" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Maştağa" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Məmmədli" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Nardaran" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Pirşağı" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Ramana" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Sabunçu" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Savalan" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Yeni Balaxanı" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Yeni Ramana" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Zabrat" });
                        }
                        else if (subgroup.Value == "Səbail r.")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "20-ci sahə" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Badamdar" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Bayıl" });
                        }
                        else if (subgroup.Value == "Suraxanı r.")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Bahar" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Bülbülə" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Dədə Qorqud" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Əmircan" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Günəşli" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Hövsan" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Qaraçuxur" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Massiv A" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Massiv B" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Massiv D" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Massiv G" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Massiv V" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Suraxanı" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Şərq" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Yeni Günəşli" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Yeni Suraxanı" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Zığ" });

                        }
                        else if (subgroup.Value == "Yasamal r.")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Yasamalı" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Yeni Yasamal" });
                        }
                    }
                }
                else if (group.Value == "Metrostansiyalar")
                {
                    List<LocationSubGroup> getLocationSubGroup = context.LocationSubGroup.Where(x => x.GroupID == group.ID).ToList();
                    foreach (var subgroup in getLocationSubGroup)
                    {
                        if (subgroup.Value == "2")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "28 May" });
                        }
                        else if (subgroup.Value == "A")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Avtovağzal" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Azadlıq Prospekti" });
                        }
                        else if (subgroup.Value == "B")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Bakmil" });
                        }
                        else if (subgroup.Value == "D")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Dərnəgül" });
                        }
                        else if (subgroup.Value == "E")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Elmlər Akademiyası" });
                        }
                        else if (subgroup.Value == "Ə")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Əhmədli" });
                        }
                        else if (subgroup.Value == "G")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Gənclik" });
                        }
                        else if (subgroup.Value == "H")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Həzi Aslanov" });
                        }
                        else if (subgroup.Value == "X")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Xalqlar Dostluğu" });
                        }
                        else if (subgroup.Value == "İ")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "İçəri Şəhər" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "İnşaatçılar" });
                        }
                        else if (subgroup.Value == "K")
                        {
                             locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Koroğlu" });
                        }
                        else if (subgroup.Value == "Q")
                        {
                             locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Qara Qarayev" });
                        }
                        else if (subgroup.Value == "M")
                        {
                             locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Memar Əcəmi" });
                        }
                        else if (subgroup.Value == "N")
                        {
                             locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Neftçilər" });
                             locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Nəriman Nərimanov" });
                             locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Nəsimi" });
                             locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Nizami" });
                        }
                        else if (subgroup.Value == "S")
                        {
                             locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Sahil" });
                        }
                        else if (subgroup.Value == "Ş")
                        {
                             locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Şah İsmayıl Xətai" });
                        }
                        else if (subgroup.Value == "U")
                        {
                             locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Ulduz" });
                        }
                    }
                }
                else if (group.Value == "Nişangahlar")
                {
                    List<LocationSubGroup> getLocationSubGroup = context.LocationSubGroup.Where(x => x.GroupID == group.ID).ToList();
                    foreach (var subgroup in getLocationSubGroup)
                    {
                        if (subgroup.Value == "A")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Ağ şəhər" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Axundov bağı" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Ayna Sultanova heykəli" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Azadlıq meydanı" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Azərbaycan Dillər Universiteti" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Azərbaycan kinoteatrı" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Azərbaycan turizm institutu" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Azneft meydanı" });

                        }
                        else if (subgroup.Value == "B")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Bakı Asiya Universiteti" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Bakı Dövlət Universiteti" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Bakı Musiqi Akademiyası" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Bakı Slavyan Universiteti" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Bayıl parkı" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Beşmərtəbə" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Botanika bağı" });
                        }
                        else if (subgroup.Value == "C")
                        {
                             locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Cavanşir körpüsü" });
                        }
                        else if (subgroup.Value == "D")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Dağüstü parkı" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Dostluq kinoteatrı" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Dövlət İdarəçilik Akademiyası" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Dövlət Statistika Komitəsi" });

                        }
                        else if (subgroup.Value == "F")
                        {
                             locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Fontanlar bağı" });
                        }
                        else if (subgroup.Value == "H")
                        {
                             locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Hüseyn Cavid parkı" });
                        }
                        else if (subgroup.Value == "X")
                        {
                             locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Xalça Muzeyi" });
                        }
                        else if (subgroup.Value == "İ")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "İçəri Şəhər" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "İdman kompleksi" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "İqsadiyyat Universiteti" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "İncəsənət və Mədəniyyət Un." });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "İzmir parkı" });

                        }
                        else if (subgroup.Value == "K")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Keşlə bazarı" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Koala parkı" });

                        }
                        else if (subgroup.Value == "Q")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Qış parkı" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Qubernator parkı" });

                        }
                        else if (subgroup.Value == "M")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "M.Ə.Sabir parkı" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "M.Hüseynzadə parkı" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Malokan bağı" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Memarlıq və İnşaat Universiteti" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Mərkəzi Univermaq" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Milli Konservatoriya" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Montin bazarı" });

                        }
                        else if (subgroup.Value == "N")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Neapol dairəsi" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Neft Akademiyası" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Neftçi bazası" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Nəriman Nərimanov parkı" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Nərimanov heykəli" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Nəsimi bazarı" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Nizami kinoteatrı" });

                        }
                        else if (subgroup.Value == "P")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Park Zorge" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Pedaqoji Universiteti" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Port Baku" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Prezident parkı" });

                        }
                        else if (subgroup.Value == "R")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Respublika stadionu" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Rəssamlıq Akademiyası" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Rusiya səfirliyi" });

                        }
                        else if (subgroup.Value == "S")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Sahil bağı" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Sea Breeze" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Sevil Qazıyeva parkı" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Səməd Vurğun parkı" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Sirk" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Sovetski" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Space TV" });

                        }
                        else if (subgroup.Value == "Ş")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Şəfa stadionu" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Şəhidlər xiyabanı" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Şəlalə parkı" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Şərq bazarı" });
                        }
                        else if (subgroup.Value == "T")
                        {
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Texniki Universiteti" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Təhsil Nazirliyi" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Tibb Universiteti" });
                            locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "TQDK" });

                        }
                        else if (subgroup.Value == "U")
                        {
                             locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Ukrayna dairəsi" });
                        }
                        else if (subgroup.Value == "Y")
                        {
                             locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Yasamal bazarı" });
                        }
                        else if (subgroup.Value == "Z")
                        {
                             locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Zabitlər parkı" }); locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Zərifə Əliyeva adına park" }); locationNames.Add(new LocationNames() { GroupID = group.ID, SubGroupID = subgroup.ID, Value = "Zoopark" });
                        }
                    }
                }
            }
            context.LocationName.AddRange(locationNames);
            context.SaveChanges();

        }
    }
}
