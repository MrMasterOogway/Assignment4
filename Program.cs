public class Client
{
    private string firstName;
    private string lastName;
    private int weight;
    private int height;

    public string FirstName
    {
        get => firstName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("First name cannot be empty.");
            firstName = value.Trim();
        }
    }

    public string LastName
    {
        get => lastName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("Last name cannot be empty.");
            lastName = value.Trim();
        }
    }

    public int Weight
    {
        get => weight;
        set
        {
            if (value <= 0)
                throw new Exception("Weight must be greater than zero.");
            weight = value;
        }
    }

    public int Height
    {
        get => height;
        set
        {
            if (value <= 0)
                throw new Exception("Height must be greater than zero.");
            height = value;
        }
    }

    public double BmiScore => Math.Round((double)weight / (height * height) * 703, 2);

    public string BmiStatus
    {
        get
        {
            if (BmiScore <= 18.4) return "Underweight";
            if (BmiScore > 18.4 && BmiScore <= 24.9) return "Normal";
            if (BmiScore > 24.9 && BmiScore <= 39.9) return "Overweight";
            return "Obese";
        }
    }

    public string FullName => $"{lastName}, {firstName}";

    public Client(string firstName, string lastName, int weight, int height)
    {
        FirstName = firstName;
        LastName = lastName;
        Weight = weight;
        Height = height;
    }
}
public class ClientManager
{
    private static Client[] clients = new Client[100];
    private static int clientCount = 0;

    public static void LoadData()
    {
        // This method would populate the clients array with initial data
        // For simulation, let's add a couple of clients manually
        clients[0] = new Client("John", "Doe", 180, 72);
        clients[1] = new Client("Jane", "Doe", 130, 65);
        clientCount = 2;
    }

    public static void SaveData()
    {
        for (int i = 0; i < clientCount; i++)
        {
            Client client = clients[i];
            Console.WriteLine($"{client.FirstName},{client.LastName},{client.Weight},{client.Height}");
        }
    }

    public static void AddClient(string firstName, string lastName, int weight, int height)
    {
        if (clientCount >= clients.Length)
            throw new Exception("Client array is full.");

        clients[clientCount++] = new Client(firstName, lastName, weight, height);
    }

    public static void DisplayClients()
    {
        for (int i = 0; i < clientCount; i++)
        {
            Client client = clients[i];
            Console.WriteLine($"{client.FullName}, Weight: {client.Weight}, Height: {client.Height}, BMI: {client.BmiScore}, Status: {client.BmiStatus}");
        }
    }

    public static void FindClient(string searchTerm)
    {
        bool found = false;
        for (int i = 0; i < clientCount; i++)
        {
            if (clients[i].FullName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            {
                Client client = clients[i];
                Console.WriteLine($"{client.FullName}, Weight: {client.Weight}, Height: {client.Height}, BMI: {client.BmiScore}, Status: {client.BmiStatus}");
                found = true;
            }
        }
        if (!found) Console.WriteLine("No client found with that name.");
    }
}

class Program
{
    static void Main()
    {
        ClientManager.LoadData();  // Load initial data

        bool exitProgram = false;
        while (!exitProgram)
        {
            Console.Clear();
            DisplayMenu();
            string command = Console.ReadLine().ToLower();

            switch (command)
            {
                case "n":
                    NewClient();
                    break;
                case "s":
                    ShowBmiInfo();
                    break;
                case "e":
                    EditClient();
                    break;
                case "l":
                    ClientManager.DisplayClients();
                    break;
                case "f":
                    FindClient();
                    break;
                case "q":
                    exitProgram = true;
                    break;
                default:
                    Console.WriteLine("Invalid selection, please try again.");
                    break;
            }

            if (!exitProgram)
            {
                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey();
            }
        }
    }

    static void DisplayMenu()
    {
        Console.WriteLine("/-----------------------------------------\\");
        Console.WriteLine("|       Personal Training App             |");
        Console.WriteLine("\\-----------------------------------------/");
        Console.WriteLine("\nMenu Options");
        Console.WriteLine("============");
        Console.WriteLine("[N]ew client");
        Console.WriteLine("[S]how client BMI info");
        Console.WriteLine("[E]dit client");
        Console.WriteLine("[L]ist all clients");
        Console.WriteLine("[F]ind client by name");
        Console.WriteLine("[Q]uit");
        Console.Write("\nEnter menu selection: ");
    }

    static void NewClient()
    {
        try
        {
            Console.WriteLine("Enter client's first name:");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter client's last name:");
            string lastName = Console.ReadLine();
            Console.WriteLine("Enter client's weight in pounds:");
            int weight = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter client's height in inches:");
            int height = Convert.ToInt32(Console.ReadLine());

            ClientManager.AddClient(firstName, lastName, weight, height);
            Console.WriteLine("Client added successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void ShowBmiInfo()
    {
        Console.WriteLine("Enter the client's full name to view BMI info:");
        string fullName = Console.ReadLine().Trim();
        
        // Leverage ClientManager to find the client
        ClientManager.FindClient(fullName);
    }

    static void EditClient()
    {
        Console.WriteLine("Enter the client's full name to edit:");
        string fullName = Console.ReadLine().Trim();
        
        // Finding and editing a client requires more interactive steps, could be complex without a List
        // For simplicity, just simulate re-adding the client with new details
        try
        {
            Console.WriteLine("Enter new first name (leave blank to keep current):");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter new last name (leave blank to keep current):");
            string lastName = Console.ReadLine();
            Console.WriteLine("Enter new weight in pounds (leave blank to keep current):");
            string weightInput = Console.ReadLine();
            Console.WriteLine("Enter new height in inches (leave blank to keep current):");
            string heightInput = Console.ReadLine();

            int weight = string.IsNullOrWhiteSpace(weightInput) ? -1 : int.Parse(weightInput);
            int height = string.IsNullOrWhiteSpace(heightInput) ? -1 : int.Parse(heightInput);

            // Reusing the FindClient method to get the index
            // Assuming the first match is the one to edit
            for (int i = 0; i < ClientManager.clients.Length; i++)
            {
                if (ClientManager.clients[i] != null && ClientManager.clients[i].FullName.Equals(fullName, StringComparison.OrdinalIgnoreCase))
                {
                    Client client = ClientManager.clients[i];
                    if (!string.IsNullOrWhiteSpace(firstName)) client.FirstName = firstName.Trim();
                    if (!string.IsNullOrWhiteSpace(lastName)) client.LastName = lastName.Trim();
                    if (weight != -1) client.Weight = weight;
                    if (height != -1) client.Height = height;
                    Console.WriteLine("Client details updated successfully!");
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating client: {ex.Message}");
        }
    }

    static void FindClient()
    {
        Console.WriteLine("Enter a name to search for:");
        string searchTerm = Console.ReadLine().Trim();
        ClientManager.FindClient(searchTerm);
    }
}
