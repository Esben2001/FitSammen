using System;
using FitSammen_API.Mapping;
using FitSammen_API.Model;
using FitSammen_API.BusinessLogicLayer;
using Xunit;

namespace FitsammenAPITest
{
    public class TestModelConversion
    {
        [Fact]
        public void ToClassListItemDTO_MapsAsExpected()
        {
            // arrange
            var cls = new Class(
                id: 5,
                trainingDate: new DateOnly(2025, 1, 1),
                instructor: new Employee(),
                description: string.Empty,
                room: new Room(),
                name: "Strength",
                capacity: 25,
                durationInMinutes: 50,
                startTime: new TimeOnly(18, 30),
                classType: ClassType.StrengthTraining
            );

            // act
            var dto = ModelConversion.ToClassListItemDTO(cls);

            // assert
            Assert.Equal(5, dto.ClassId);
            Assert.Equal(new DateOnly(2025, 1, 1), dto.Date);
            Assert.Equal("Strength", dto.ClassName);
            Assert.Equal(ClassType.StrengthTraining, dto.ClassType);
            Assert.Equal(new TimeOnly(18, 30), dto.StartTime);
            Assert.Equal(50, dto.DurationInMinutes);
            Assert.Equal(25, dto.Capacity);
        }

        [Fact]
        public void ToBookingResponseDTO_MapsAsExpected_OnSuccess()
        {
            // arrange
            var result = new BookingResult
            {
                Status = BookingStatus.Success,
                BookingID = 123
            };

            // act
            var dto = ModelConversion.ToBookingResponseDTO(result);

            // assert
            Assert.Equal(123, dto.BookingId);
            Assert.Equal("Success", dto.Status);
            Assert.Equal("Booking successful.", dto.Message);
        }

        [Fact]
        public void ToBookingResponseDTO_MapsClassFull_AndNullBookingIdToZero()
        {
            // arrange
            var result = new BookingResult
            {
                Status = BookingStatus.ClassFull,
                BookingID = null
            };

            // act
            var dto = ModelConversion.ToBookingResponseDTO(result);

            // assert
            Assert.Equal(0, dto.BookingId);
            Assert.Equal("ClassFull", dto.Status);
            Assert.Equal("Booking failed: Member has already booked this class.", dto.Message);
        }

        [Fact]
        public void ToBookingResponseDTO_MapsErrorWithDefaultMessage()
        {
            // arrange
            var result = new BookingResult
            {
                Status = BookingStatus.Error,
                BookingID = null
            };

            // act
            var dto = ModelConversion.ToBookingResponseDTO(result);

            // assert
            Assert.Equal(0, dto.BookingId);
            Assert.Equal("Error", dto.Status);
            Assert.Equal("Booking failed: Unknown error.", dto.Message);
        }

        [Fact]
        public void ToBookingResponseDTO_UnknownStatus_UsesDefaultMessage()
        {
            // arrange
            var result = new BookingResult
            {
                Status = (BookingStatus)999,
                BookingID = null
            };

            // act
            var dto = ModelConversion.ToBookingResponseDTO(result);

            // assert
            Assert.Equal(0, dto.BookingId);
            Assert.Equal("999", dto.Status); // Enum ToString for undefined value yields numeric string
            Assert.Equal("Booking failed: Unknown error.", dto.Message);
        }
    }
}