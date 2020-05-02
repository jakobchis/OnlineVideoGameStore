using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVGS;
using CVGS.Data;
using CVGS.Models;

namespace CVGS.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var userManager = serviceProvider
                .GetRequiredService<UserManager<ApplicationUser>>()
            )
            {
                if (userManager.FindByNameAsync("admin").Result != null)
                {
                    return; // DB has been seeded
                }
                else
                {
                    ApplicationUser user = new ApplicationUser();
                    user.Id = Guid.Parse("3b0d0f76-e144-4088-8c40-e89541ef31a7");
                    user.UserName = "admin";
                    user.Email = "admin@cvgs.com";
                    user.FirstName = "Local";
                    user.LastName = "Admin";

                    IdentityResult result = userManager.CreateAsync(user, "Password123!").Result;

					/*
                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Administrator").Wait();
                    }
                    */
				}

				if (userManager.FindByNameAsync("user").Result != null)
				{
					return;
				}
				else
				{
					ApplicationUser user = new ApplicationUser();
					user.Id = Guid.Parse("f2a49c62-f92b-41d4-88a3-e173266e061e");
					user.UserName = "user";
					user.Email = "user@cvgs.com";
					user.FirstName = "User";
					user.LastName = "McUsersson";

					IdentityResult result = userManager.CreateAsync(user, "Password123!").Result;
				}
            }
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
				context.Admin.Add(
					new Admin
					{
						Id = Guid.Parse("25d12032-26fd-4f45-ab7a-4cee02fddb69"),
						UserId = Guid.Parse("3b0d0f76-e144-4088-8c40-e89541ef31a7")
					});

                // Look for any genders.
                if (context.Gender.Any())
                {
                    return;   // DB has been seeded
                }

				context.Game.Add(
					new Game
					{
						Id = Guid.Parse("a91ee609-7170-4cf3-80a9-114570a07de8"),
						Name = "Test Game",
						Description = "Test game description"
					}
				);

				context.Country.AddRange(
                    new Country
                    {
                        Id = Guid.Parse("65d66900-7354-4a39-9f5a-ae769165e52f"),
                        Name = "Canada",
                        Code = "CA"
                    },
                    new Country
                    {
                        Id = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096"),
                        Name = "United States",
                        Code = "US"
                    }
                );

                context.Province.AddRange(
                    // Canadian Provinces
                    new Province
                    {
                        Id = Guid.Parse("0699eb27-7c4d-46e9-b75b-a65eff68eefa"),
                        Name = "Alberta",
                        Code = "AB",
                        CountryId = Guid.Parse("65d66900-7354-4a39-9f5a-ae769165e52f")
                    },
                    new Province
                    {
                        Id = Guid.Parse("56f6b5aa-f3a4-492c-b48f-190acbb7007f"),
                        Name = "British Columbia",
                        Code = "BC",
                        CountryId = Guid.Parse("65d66900-7354-4a39-9f5a-ae769165e52f")
                    },
                    new Province
                    {
                        Id = Guid.Parse("316fca64-e876-4dae-911b-eae114e6c191"),
                        Name = "Manitoba",
                        Code = "MB",
                        CountryId = Guid.Parse("65d66900-7354-4a39-9f5a-ae769165e52f")
                    },
                    new Province
                    {
                        Id = Guid.Parse("25773605-703b-4ec3-9ccc-062483ea2bff"),
                        Name = "New Brunswick",
                        Code = "NB",
                        CountryId = Guid.Parse("65d66900-7354-4a39-9f5a-ae769165e52f")
                    },
                    new Province
                    {
                        Id = Guid.Parse("7b3f263f-56ef-42a7-9d2c-d934238a8b3f"),
                        Name = "Newfoundland and Labrador",
                        Code = "NL",
                        CountryId = Guid.Parse("65d66900-7354-4a39-9f5a-ae769165e52f")
                    },
                    new Province
                    {
                        Id = Guid.Parse("d1cf66a6-306a-4aa8-99ea-c3bf2c5f0d76"),
                        Name = "Northwest Territories",
                        Code = "NT",
                        CountryId = Guid.Parse("65d66900-7354-4a39-9f5a-ae769165e52f")
                    },
                    new Province
                    {
                        Id = Guid.Parse("e0a86c8d-f0fb-414a-8498-1fa56a523547"),
                        Name = "Nova Scotia",
                        Code = "NS",
                        CountryId = Guid.Parse("65d66900-7354-4a39-9f5a-ae769165e52f")
                    },
                    new Province
                    {
                        Id = Guid.Parse("d8cacc0d-fbaa-4bc7-a4c6-7ac7620fbcf8"),
                        Name = "Nunavut",
                        Code = "NU",
                        CountryId = Guid.Parse("65d66900-7354-4a39-9f5a-ae769165e52f")
                    },
                    new Province
                    {
                        Id = Guid.Parse("a68aa251-da27-4d4a-bcf9-418222ab73b1"),
                        Name = "Ontario",
                        Code = "ON",
                        CountryId = Guid.Parse("65d66900-7354-4a39-9f5a-ae769165e52f")
                    },
                    new Province
                    {
                        Id = Guid.Parse("ebb3e8ac-26c6-4523-a0e4-6859b173d1a2"),
                        Name = "Prince Edward Island",
                        Code = "PE",
                        CountryId = Guid.Parse("65d66900-7354-4a39-9f5a-ae769165e52f")
                    },
                    new Province
                    {
                        Id = Guid.Parse("2293182c-6241-4fc2-998d-a3824ab44b01"),
                        Name = "Quebec",
                        Code = "QC",
                        CountryId = Guid.Parse("65d66900-7354-4a39-9f5a-ae769165e52f")
                    },
                    new Province
                    {
                        Id = Guid.Parse("c4731f0f-8833-4f5e-8e0c-7b6ea8e6fa2e"),
                        Name = "Saskatchewan",
                        Code = "SK",
                        CountryId = Guid.Parse("65d66900-7354-4a39-9f5a-ae769165e52f")
                    },
                    new Province
                    {
                        Id = Guid.Parse("06af096e-edaa-4825-a0ba-df9dba19c293"),
                        Name = "Yukon",
                        Code = "YT",
                        CountryId = Guid.Parse("65d66900-7354-4a39-9f5a-ae769165e52f")
                    },

                    // US States
                    new Province
                    {
                        Id = Guid.Parse("0a2380bd-d995-4dc8-8522-0be8561a69a0"),
                        Name = "Alabama",
                        Code = "AL",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("0bfb9320-f3ad-472a-bd90-b27d7454cc71"),
                        Name = "Alaska",
                        Code = "AK",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("50cc848d-05f8-47bc-abe7-83f0ab83665e"),
                        Name = "Arizona",
                        Code = "AZ",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("1efc5232-f4f0-4986-98e8-621efe76e50a"),
                        Name = "Arkansas",
                        Code = "AR",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("318b07ad-ecd5-465c-9b7a-a2c42e718c1c"),
                        Name = "California",
                        Code = "CA",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("b00cf9e4-1d31-4f33-a9a1-b80668e4316e"),
                        Name = "Colorado",
                        Code = "CO",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("9570ac70-c052-41d1-8cea-53a1e6033a15"),
                        Name = "Connecticut",
                        Code = "CT",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("0d874422-fc5d-440c-b893-7a626df717e2"),
                        Name = "Delaware",
                        Code = "DE",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("d2f987a2-7766-4a10-b7de-d09e770610ce"),
                        Name = "Florida",
                        Code = "FL",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("32fd4f44-97b7-4538-9873-f6cd589b7dd8"),
                        Name = "Georgia",
                        Code = "GA",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("baf4a3b4-72c8-466a-81a7-a772f0c8880a"),
                        Name = "Hawaii",
                        Code = "HI",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("439f78a4-d8c4-4dbf-b685-9020ac2961a2"),
                        Name = "Idaho",
                        Code = "ID",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("4768ce9b-6e46-48d4-8b40-4824637de24d"),
                        Name = "Illinois",
                        Code = "IL",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("c1c03131-03ed-4720-ba29-bcc549912ae6"),
                        Name = "Indiana",
                        Code = "IN",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("833972d5-5363-4ae4-a535-b87bbf65b47c"),
                        Name = "Iowa",
                        Code = "IA",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("f2ea4076-1464-4337-b62f-3c960773040c"),
                        Name = "Kansas",
                        Code = "KS",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("65e98b54-acce-43cc-b9cc-11143f69b5ad"),
                        Name = "Kentucky",
                        Code = "KY",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("ab148f1d-a45d-42fc-9069-c95f24dceeae"),
                        Name = "Louisiana",
                        Code = "LA",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("2b74fe0c-1524-4276-bbbf-8def22b1bc8a"),
                        Name = "Maine",
                        Code = "ME",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("04cf42b9-a922-471e-90af-e520d411acd3"),
                        Name = "Maryland",
                        Code = "MD",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("951ac223-f293-4e46-b768-b3d829f1d2d5"),
                        Name = "Massachusetts",
                        Code = "MA",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("421a26aa-0165-4e6c-b23b-ccfca2ccb35c"),
                        Name = "Michigan",
                        Code = "MI",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("970dbe41-3cb6-4b7c-986a-cbf06d77eb65"),
                        Name = "Minnesota",
                        Code = "MN",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("7f08a192-b98d-470b-929a-1601c0455dde"),
                        Name = "Mississippi",
                        Code = "MS",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("767bbdee-986d-4eb5-80d4-f41f6e2734dd"),
                        Name = "Missouri",
                        Code = "MO",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("e7d9d4d6-8fc7-403e-a19e-2eda5f75bd73"),
                        Name = "Montana",
                        Code = "MT",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("b7736053-b0b5-4d8e-a639-1b9392e59331"),
                        Name = "Nebraska",
                        Code = "NE",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("7fa68f27-6fa3-4d2d-b56b-52c82f53cf77"),
                        Name = "Nevada",
                        Code = "NV",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("8b42b99d-6ff4-44a7-bae9-1abdf6aeee59"),
                        Name = "New Hampshire",
                        Code = "NH",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("e5fcffe2-36da-462e-880e-47c265c44827"),
                        Name = "New Jersey",
                        Code = "NJ",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("9b38689e-2368-4e3b-878d-9ddd56f5811a"),
                        Name = "New Mexico",
                        Code = "NM",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("c400b34d-357b-4da7-921c-8253aff61ec9"),
                        Name = "New York",
                        Code = "NY",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("a88aae75-cfdd-4dbf-85ee-e5ba0cc25bea"),
                        Name = "North Carolina",
                        Code = "NC",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("313b60b5-4c1e-412b-8635-27578d6cb047"),
                        Name = "North Dakota",
                        Code = "ND",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("1b42d769-f16b-431e-be0b-c0098388273a"),
                        Name = "Ohio",
                        Code = "OH",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("cf963c0e-d0fb-4d7f-87f6-a59fdaa5f8de"),
                        Name = "Oklahoma",
                        Code = "OK",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("5e3834c5-0030-41bd-b82d-617f2a343a4d"),
                        Name = "Oregon",
                        Code = "OR",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("9c85e0c6-413d-4bce-98b3-04082d6bad8a"),
                        Name = "Pennsylvania",
                        Code = "PA",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("ea4d730c-8b59-4b49-aba4-b82df2ea6143"),
                        Name = "Rhode Island",
                        Code = "RI",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("a5324622-a9d7-4595-bac3-fbd12110d693"),
                        Name = "South Carolina",
                        Code = "SC",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("b87b1c43-1722-4d98-ad62-ee5f7a34e966"),
                        Name = "South Dakota",
                        Code = "SD",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("d90b44df-4bf5-43d1-b69e-445edeee6362"),
                        Name = "Tennessee",
                        Code = "TN",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("8e6d3084-3b4e-4370-bf7c-15165685324b"),
                        Name = "Texas",
                        Code = "TX",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("24025de1-d3a6-45a5-9c9a-a093f3a1f568"),
                        Name = "Utah",
                        Code = "UT",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("59b366bc-99ef-4b41-ad95-d068818c0834"),
                        Name = "Vermont",
                        Code = "VT",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("1a6213ac-2d42-463d-9b5c-3211de0d700e"),
                        Name = "Virginia",
                        Code = "VA",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("8b577762-c733-44e7-8bd6-7243120b6d47"),
                        Name = "Washington",
                        Code = "WA",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("7778e634-2297-4e5b-ad38-8ae20f7967ef"),
                        Name = "West Virginia",
                        Code = "WV",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("ff0e5532-deeb-4cd9-af7a-b54577ee2887"),
                        Name = "Wisconsin",
                        Code = "WI",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    },
                    new Province
                    {
                        Id = Guid.Parse("bac1a939-db8a-466c-b9e0-7c889429ce28"),
                        Name = "Wyoming",
                        Code = "WY",
                        CountryId = Guid.Parse("7a48d27e-163f-4a3f-85e8-fa715da90096")
                    }
                );

                context.Gender.AddRange(
                    new Gender
                    {
                        Id = Guid.Parse("f9399e11-a101-467b-8818-4f76268cd806"),
                        Name = "Male",
                        Code = "M",
                    },
                    new Gender
                    {
                        Id = Guid.Parse("ba8ec0e4-412a-47a5-adf2-f7d5864e9701"),
                        Name = "Female",
                        Code = "F",
                    },
                    new Gender
                    {
                        Id = Guid.Parse("40034481-9024-4989-9562-08d20dc0d1c6"),
                        Name = "Other",
                        Code = "X",
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
