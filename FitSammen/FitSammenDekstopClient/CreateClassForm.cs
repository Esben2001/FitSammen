using FitSammenDekstopClient.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FitSammenDekstopClient
{
    public partial class CreateClassForm : Form
    {
        public Class? CreatedClass { get; private set; }

        private List<Location> _locations = new();

        private List<Room> _roomsForCurrentLocation = new();

        //private readonly ILocationLogic _locationLogic;

        //private readonly IRoomLogic _roomLogic;

        private readonly List<Employee> _employees = new List<Employee>
        {
            new Employee("Lise", "Hansen", "lise@fitsammen.dk", "11111111",
                new DateOnly(1990,5,12), 1, UserType.Employee, "111111-1111"),
            new Employee("Mads", "Jensen", "mads@fitsammen.dk", "22222222",
                new DateOnly(1988,3,20), 2, UserType.Employee, "222222-2222"),
            new Employee("Sofie", "Lund", "sofie@fitsammen.dk", "33333333",
                new DateOnly(1992,8,5), 3, UserType.Employee, "333333-3333")
        };

        public CreateClassForm()
        {
            InitializeComponent();
            SetupStartTimeCombo();
            SetupClassTypeCombo();
            SetupInstructorCombo();
            SetupLocationCombo();

        }

        private void btnCreateClass_Click(object sender, EventArgs e)
        {
            //Valider input
            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show("Navn på hold skal udfyldes.");
                textBoxName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxDescription.Text))
            {
                MessageBox.Show("Beskrivelse skal udfyldes.");
                textBoxDescription.Focus();
                return;
            }

            if (comboBoxEmployee.SelectedItem is not Employee instructor)
            {
                MessageBox.Show("Vælg en instruktør.");
                comboBoxEmployee.Focus();
                return;
            }

            if (comboBoxRoom.SelectedItem is not Room room)
            {
                MessageBox.Show("Vælg et lokale.");
                comboBoxRoom.Focus();
                return;
            }

            if (comboBoxClassType.SelectedItem is not ClassType classType)
            {
                MessageBox.Show("Vælg en holdtype.");
                comboBoxClassType.Focus();
                return;
            }

            if (comboBoxStartTime.SelectedItem is not TimeOnly startTime)
            {
                MessageBox.Show("Vælg et starttidspunkt.");
                comboBoxStartTime.Focus();
                return;
            }

            if ((int)UpDownCapacity.Value == 0)
            {
                MessageBox.Show("Kapacitet skal være større end 0.");
                UpDownCapacity.Focus();
                return;
            }

            if ((int)UpDownDuration.Value == 0)
            {
                MessageBox.Show("Varighed skal være større end 0 minutter.");
                UpDownDuration.Focus();
                return;
            }

            string name = textBoxName.Text.Trim();
            string description = textBoxDescription.Text.Trim();

            int capacity = (int)UpDownCapacity.Value;
            int duration = (int)UpDownDuration.Value;

            DateOnly trainingDate = DateOnly.FromDateTime(dateTimePickerTrainingDate.Value.Date);
           
            int id = 0; // Id vil blive sat af databasen ved oprettelse
            int memberCount = 0; // Nyt hold har ingen medlemmer ved oprettelse

            // Opret Class
            var newClass = new Class(
                id: id,
                trainingDate: trainingDate,
                instructor: instructor,
                description: description,
                room: room,
                name: name,
                capacity: capacity,
                memberCount: memberCount,
                durationInMinutes: duration,
                startTime: startTime,
                classType: classType
            );

            // Her skal omids betale til create ske

            
            CreatedClass = newClass;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SetupStartTimeCombo()
        {
            var times = new List<TimeOnly>();
            var start = new TimeOnly(6, 0);
            var end = new TimeOnly(22, 0);

            var current = start;
            while (current <= end)
            {
                times.Add(current);
                current = current.AddMinutes(60);
            }

            comboBoxStartTime.DataSource = times;
            comboBoxStartTime.Format += (s, e) =>
            {
                if (e.ListItem is TimeOnly t)
                {
                    e.Value = t.ToString("HH\\:mm");
                }
            };
            comboBoxStartTime.SelectedIndex = -1;
        }
        private void SetupClassTypeCombo()
        {
            comboBoxClassType.DataSource = Enum.GetValues(typeof(ClassType));
            comboBoxClassType.SelectedIndex = -1;
        }

        private void SetupInstructorCombo()
        {
            comboBoxEmployee.DataSource = _employees;
            comboBoxEmployee.DisplayMember = "FullName";
            comboBoxEmployee.ValueMember = "User_ID";

            comboBoxEmployee.SelectedIndex = -1;
        }

        private void SetupLocationCombo()
        {
            _locations = CreateTestLocations();

            comboBoxLocation.DataSource = _locations;
            comboBoxLocation.DisplayMember = "Address"; 
            comboBoxLocation.ValueMember = "Zipcode";

            // SelectedIndexChanged er en EventHandler som er en delegate. Så vi kan tilknytte en metode til den.
            comboBoxLocation.SelectedIndexChanged += comboBoxLocation_SelectedIndexChanged;

            // Vi kunne også bruge en lambda-udtryk som nedenfor og droppe mellemmetoden comboBoxLocation_SelectedIndexChanged()
            //comboBoxLocation.SelectedIndexChanged += (s, e) => LoadRoomsForSelectedLocation();

            comboBoxLocation.SelectedIndex = -1; 
        }

        // Hvergang vi ændrer lokation i comboboxen, skal vi opdatere lokalerne i den anden combobox
        private void comboBoxLocation_SelectedIndexChanged(object? sender, EventArgs e)
        {
            LoadRoomsForSelectedLocation();
        }

        private void LoadRoomsForSelectedLocation()
        {
            var selectedLocation = comboBoxLocation.SelectedItem as Location;
            if (selectedLocation == null)
                return;

            // _roomsForCurrentLocation = _roomLogic.GetRoomsByLocation(selectedLocation).ToList();

            _roomsForCurrentLocation = CreateRoomsForLocation(selectedLocation);

            comboBoxRoom.DataSource = _roomsForCurrentLocation;
            comboBoxRoom.DisplayMember = "RoomName";
            comboBoxRoom.ValueMember = "RoomId";

        }

        public static List<Location> CreateTestLocations()
        {
            var loc1 = new Location("Fitnessvej", 10, 9000, "Aalborg", "Danmark");
            var loc2 = new Location("Træningsvej", 5, 8000, "Aarhus", "Danmark");
            var loc3 = new Location("Motionstorvet", 2, 2100, "København", "Danmark");

            return new List<Location> { loc1, loc2, loc3 };
        }

        public static List<Room> CreateRoomsForLocation(Location loc)
        {
            if (loc.Zipcode.ZipcodeNumber == 9000) 
            {
                return new List<Room>
            {
                new Room(1, "Sal 1", 20, loc),
                new Room(2, "Spinningsal", 25, loc),
                new Room(3, "Yoga-rum", 15, loc)
            };
            }

            if (loc.Zipcode.ZipcodeNumber == 8000) 
            {
                return new List<Room>
            {
                new Room(4, "Styrkesal", 18, loc),
                new Room(5, "Cardio-rum", 22, loc)
            };
            }

            return new List<Room>
        {
            new Room(6, "Crossfit Box", 30, loc),
            new Room(7, "Dance Studio", 20, loc)
        };
        }

    }
}
