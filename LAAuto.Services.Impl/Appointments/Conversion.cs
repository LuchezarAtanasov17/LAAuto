using LAAuto.Services.Appointments;
using ENTITIES = LAAuto.Entities.Models;

namespace LAAuto.Services.Impl.Appointments
{
    /// <summary>
    /// Represents a conversion class for converting appointment models.
    /// </summary>
    public static class Conversion
    {
        /// <summary>
        /// Converts an entity appointment model to service model.
        /// </summary>
        /// <param name="source">the source</param>
        /// <returns>the service model</returns>
        public static Appointment ConvertAppointment(ENTITIES.Appointment source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var target = new Appointment()
            {
                Id = source.Id,
                CategoryId = source.CategoryId,
                ServiceId = source.ServiceId,
                UserId = source.UserId,
                StartDate = source.StartDate,
                EndDate = source.EndDate,
                Description = source.Description
            };

            return target;
        }

        /// <summary>
        /// Converts the service request to entity model.
        /// </summary>
        /// <param name="source">the source</param>
        /// <returns>the entity model</returns>
        public static ENTITIES.Appointment ConvertAppointment(CreateAppointmentRequest source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var target = new ENTITIES.Appointment()
            {
                ServiceId = source.ServiceId,
                CategoryId = source.CategoryId,
                UserId = source.UserId,
                StartDate = source.StartDate,
                EndDate = source.EndDate,
                Description = source.Description,
            };

            return target;
        }
    }
}
