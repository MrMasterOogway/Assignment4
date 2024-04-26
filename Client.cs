using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clients {
  public class Client {
    private string firstname;
    private string lastname;
    private double weight;
    private double height;

    public Client() {
      Firstname = "xxx";
      Lastname = "xxx";
      Weight = 0;
      Height = 0;
    }

    public Client (string firstname, string lastname, double weight, double height) {
      Firstname = firstname;
      Lastname  = lastname;
      Weight = weight;
      Height = height;
    }

    public string Firstname {
			get { 
        return firstname;
        } set {
				if (string.IsNullOrWhiteSpace(value))
					throw new ArgumentNullException("Firstname is required.");
        firstname = value;
			}
		}

    public string Lastname {
			get { 
        return lastname;
      } set {
				if (string.IsNullOrWhiteSpace(value))
					throw new ArgumentNullException("Lastname is required.");
        lastname = value;
			}
		}
    
    public double Weight {
			get { 
        return weight;
      } set {
				if (value < 0.0)
					throw new ArgumentException("Weight must be greater than zero.");
        weight = value;
			}
		}

    public double Height {
			get { 
        return height;
      } set {
				if (value < 0.0) 
					throw new ArgumentException("Height must be greater than zero.");
        height = value;
			}
		}

    public double BmiScore {
      get {
        double score = (Weight / (Height * Height) * 703);
        return score;
      }
    }

    public string BmiStatus {
      get {
        string status = "";
        double bmiScore = BmiScore;
        if(bmiScore >= 0 && bmiScore <= 18.4) 
          status = "Underweight";
        if(bmiScore >= 18.5 && bmiScore <= 24.9) 
          status = "Normal";
        if(bmiScore >= 25 && bmiScore <= 39.9) 
          status = "Overweight";
        if(bmiScore >= 40)
          status = "Obese";
        return status;
      }
    }

    public override string ToString() {
			return $"{Firstname},{Lastname},{Weight},{Height}";
		}
  }
}