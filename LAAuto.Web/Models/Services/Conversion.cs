using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Net.Mime;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using SERVICES_SERVICES = LAAuto.Services.Services;
using WEB_CATEGORY = LAAuto.Web.Models.Categories;
using WEB_USERS = LAAuto.Web.Models.Users;

namespace LAAuto.Web.Models.Services
{
    public class Conversion
    {
        public static ServiceViewModel ConvertService(SERVICES_SERVICES.Service source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var target = new ServiceViewModel()
            {
                Id = source.Id,
                UserId = source.UserId,
                Name = source.Name,
                Description = source.Description,
                OpenTime = source.OpenTime,
                CloseTime = source.CloseTime,
                Location = source.Location,
                User = WEB_USERS.Conversion.ConvertUser(source.User),
                //Image = source.Image != null && source.Image.Length > 0
                //    ? ConvertImage(source.Image)
                //    : null,
                Categories = source.Categories
                    .Select(WEB_CATEGORY.Conversion.ConvertCategory)
                    .ToHashSet()
                //Appointments = source.Appointments
                //    .Select(WEB_APPOINTMENT.Conversion.ConvertAppointment)
                //    .ToHashSet(),

                //AverageRating = source.AverageRating,
            };

            return target;
        }

    public static SERVICES_SERVICES.CreateServiceRequest ConvertService(CreateServiceRequest source)
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        var target = new SERVICES_SERVICES.CreateServiceRequest
        {
            UserId = source.UserId,
            Name = source.Name,
            Description = source.Description,
            OpenTime = TimeOnly.Parse(source.OpenTime),
            CloseTime = TimeOnly.Parse(source.CloseTime),
            Location = source.Location,
            //Image = source.Image != null
            //    ? ConvertImage(source.Image)
            //    : null
        };

        return target;
    }

    public static SERVICES_SERVICES.UpdateServiceRequest ConvertService(UpdateServiceRequest source)
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        var target = new SERVICES_SERVICES.UpdateServiceRequest
        {
            Id = source.Id,
            Name = source.Name,
            Description = source.Description,
            OpenTime = TimeOnly.Parse(source.OpenTime),
            CloseTime = TimeOnly.Parse(source.CloseTime),
            Location = source.Location,
            //Image = source.Image
        };

        return target;
    }

        //private static IFormFile ConvertImage(byte[] bytes)
        //{
        //    using (var ms = new MemoryStream(bytes))
        //    {
        //        file.CopyTo(ms);
        //        // act on the Base64 data
        //
        //        return file;
        //    }
        //}

        private static byte[] ConvertImage(IFormFile image)
    {
        using (var ms = new MemoryStream())
        {
            image.CopyTo(ms);
            var fileBytes = ms.ToArray();
            // act on the Base64 data

            return fileBytes;
        }
    }
}
}
