using Clients;

Client client = new();
List<Client> Clientlist = [];
bool loop = true;
LoadFile(Clientlist);

Console.Clear();
while(loop) {
  try {
    displayMainMenu();
    string mainMenuChoice = Prompt("\nEnter a Main Menu Choice: ").ToUpper();
    if(mainMenuChoice == "L")
      displayAllClients();
    if(mainMenuChoice == "F")
      findClient();
    if(mainMenuChoice == "A")
      AddClientToList(Clientlist);
    if(mainMenuChoice == "E")
      EditClient();
    if(mainMenuChoice == "D")
     DeleteClient();
    if(mainMenuChoice == "S")
      showClientBmiInfo();
    if (mainMenuChoice == "Q") {
      SaveClients();
			loop = false;
			throw new Exception("Bye, hope to see you again.");
    }
  }  catch (Exception ex) {
    Console.WriteLine(ex.Message);
  } 
}

void displayMainMenu() {
  Console.WriteLine($"\n MENU OPTIONS");  
  Console.WriteLine($"(L)ist All Clients");
  Console.WriteLine($"(F)ind Client Information");
  Console.WriteLine($"(A)dd New Client Record");
  Console.WriteLine($"(E)dit Client Information");
  Console.WriteLine($"(D)elete Client Record");
  Console.WriteLine($"(S)how Client BMI Information");
  Console.WriteLine($"(Q)uit");
}

void displayEditMenu() {
  Console.WriteLine($"(F)irst Name");
  Console.WriteLine($"(L)ast Name");
  Console.WriteLine($"(H)eight");
  Console.WriteLine($"(W)eight");
  Console.WriteLine($"(R)eturn to Main Menu");
}

string Prompt(string prompt){
	string input = "";
	while (true) {
		try {
		Console.Write(prompt);
		input = Console.ReadLine().Trim();
		if(string.IsNullOrEmpty(input))
			throw new Exception($"Empty Input: Please enter something.");
		break;
		}catch (Exception ex) {
			Console.WriteLine(ex.Message);
		}
	}
	return input;
}

double SecondPrompt(String msg, double min) {
	double num = 0;
	while (true) {
		try {
			Console.Write($"{msg} between {min}: ");
			num = double.Parse(Console.ReadLine());
			if (num < min)
				throw new Exception($"Must be greater than {min:n2}");
			break;
		} catch (Exception ex) {
			Console.WriteLine($"Invalid: {ex.Message}");
		}
	}
	return num;
}

void LoadFile(List<Client> Clientlist) {
  while(true) {
    try {
			string fileName = "client.csv";
			string filePath = $"./data/{fileName}";
			if (!File.Exists(filePath))
				throw new Exception($"The file {fileName} does not exist.");
			string[] csvFileInput = File.ReadAllLines(filePath);
			for(int i = 0; i < csvFileInput.Length; i++) {
				string[] items = csvFileInput[i].Split(',');
				Client client = new(items[0], items[1], double.Parse(items[2]), double.Parse(items[3]));
        Clientlist.Add(client);
			}
			Console.WriteLine($"Load complete. {fileName} has {Clientlist.Count} data entries");
			break;
    } catch(Exception ex) {
      Console.WriteLine(ex.Message);
    }
  }
}

void displayAllClients() {
  try {
    if(Clientlist.Count <= 0)
      throw new Exception($"No data has been loaded");
    foreach(Client client in Clientlist) 
      showClientInfo(client);
  } catch (Exception ex) {
    Console.WriteLine($"Error: {ex.Message}");
  }
}

void findClient() {
  displayAllClients();
  string clientName = Prompt("Enter client's Firstname: ");
  List<Client> filteredClient = Clientlist.Where(c => c.Firstname.Contains(clientName)).ToList();
  Client selectedClient = filteredClient.FirstOrDefault();
  Console.WriteLine($"\n{selectedClient.ToString()}");
  Console.WriteLine($"Client's BMI Score:\t{selectedClient.BmiScore:n2}");
	Console.WriteLine($"Client's BMI Status:\t{selectedClient.BmiStatus}");

}

void SaveClients() {
  string fileName = "client.csv";
  string filePath = $"./data/{fileName}";
  List<String> ClientRecords = [];
  foreach(Client data in Clientlist) {
    ClientRecords.Add($"{data.Firstname}, {data.Lastname}, {data.Weight}, {data.Height}");
  }
  File.WriteAllLines(filePath, ClientRecords);
}

void EditClient() {
  displayAllClients();
  string clientFullName = Prompt("Enter client's Firstname: ");
    List<Client> filteredClient = Clientlist.Where(c => c.Firstname.Contains(clientFullName)).ToList();
    Client selectedClient = filteredClient.FirstOrDefault();
      while(true) {
        Console.WriteLine($"===== SELECT DATA OF TO EDIT ====="); 
        displayEditMenu();
        string edit = Prompt("\nEnter Edit Menu Choice: ").ToUpper();
        if(edit == "F") {
          selectedClient.Firstname = Prompt($"Enter Client Firstname: ");
        } else if(edit == "L") {
          selectedClient.Lastname = Prompt($"Enter Client Lastname: ");
        } else if(edit == "W") {
          selectedClient.Weight = SecondPrompt($"Enter Client Weight (lbs): ", 0);
        } else if(edit == "H") {
          selectedClient.Height = SecondPrompt($"Enter Client Height (inches): ", 0);
        } else if(edit == "R") {
          Console.WriteLine($"You have successfully updated details");
          break;
        } else {
          throw new Exception("Invalid Edit Menu Choice. Please Try Again.");
        }
      }          
}

void DeleteClient() { 
  displayAllClients();
  string clientFullName = Prompt("Enter client's Firstname: ");
  List<Client> filteredClient = Clientlist.Where(c => c.Firstname.Contains(clientFullName)).ToList();
  Client selectedClient = filteredClient.FirstOrDefault();
      while(true) {
        string yesno = Prompt($"You are about to delete "+ selectedClient.Firstname +"'s record. Proceed? Y/N: ").ToUpper();
        if (yesno == "Y") {
          Clientlist.Remove(selectedClient);
          Console.WriteLine($"{selectedClient.Firstname}'s has been deleted.");
          break;
        } else if (yesno == "N") {
          Console.WriteLine($"Delete operation cancelled for {selectedClient.Firstname}.");
          break;
        } else {
          Console.WriteLine($"Invalid confirmation input. Please enter 'Y' or 'N'.");
        }
      }
}

void AddClientToList(List<Client> Clientlist) {
  GetFirstname(client);
  GetLastname(client);
  GetWeight(client);
  GetHeight(client);
  Clientlist.Add(client);
}

void GetFirstname(Client client) {
	string prompt = Prompt($"Enter Firstname: ");
	client.Firstname = prompt;
}

void GetLastname(Client client) {
	string prompt = Prompt($"Enter Lastname: ");
	client.Lastname = prompt;
}

void GetWeight(Client client) {
	double Double = SecondPrompt("Enter Weight in inches: ", 0);
	client.Weight = Double;
}

void GetHeight(Client client) {
	double Double = SecondPrompt("Enter Height in inches: ", 0);
	client.Height = Double;
}

void showClientInfo(Client client) {
  if(client == null)
    throw new Exception("No Client In Memory");
  Console.WriteLine($"\n{client.ToString()}");
  Console.WriteLine($"Client's BMI Score:\t{client.BmiScore:n2}");
	Console.WriteLine($"Client's BMI Status:\t{client.BmiStatus}");
}

void showClientBmiInfo() {
  string clientFullName = Prompt("\nEnter client's Firstname: ");
  List<Client> filteredClient = Clientlist.Where(c => c.Firstname.Contains(clientFullName)).ToList();
  Client selectedClient = filteredClient.FirstOrDefault();
  Console.WriteLine($"\n{selectedClient.ToString()}");
  Console.WriteLine($"Client's BMI Score:\t{selectedClient.BmiScore:n2}");
	Console.WriteLine($"Client's BMI Status:\t{selectedClient.BmiStatus}");
}
