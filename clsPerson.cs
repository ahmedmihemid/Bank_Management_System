using System;

public class clsPerson 
{
    private string _firstName;
    private string _lastName;
    private string _email;
    private string _phone;

    // Constructor
    public clsPerson(string firstName, string lastName, string email, string phone)
    {
        _firstName = firstName;
        _lastName = lastName;
        _email = email;
        _phone = phone;
    }

    // FirstName Property
    public string FirstName
    {
        get { return _firstName; }
        set { _firstName = value; }
    }

    // LastName Property
    public string LastName
    {
        get { return _lastName; }
        set { _lastName = value; }
    }

    // Email Property
    public string Email
    {
        get { return _email; }
        set { _email = value; }
    }

    // Phone Property
    public string Phone
    {
        get { return _phone; }
        set { _phone = value; }
    }

    // FullName method to get the full name
    public string FullName()
    {
        return _firstName + " " + _lastName;
    }

    // Print method to display person information
    public void Print()
    {
        Console.WriteLine("\nInfo:");
        Console.WriteLine("___________________");
        Console.WriteLine("FirstName: " + _firstName);
        Console.WriteLine("LastName : " + _lastName);
        Console.WriteLine("Full Name: " + FullName());
        Console.WriteLine("Email    : " + _email);
        Console.WriteLine("Phone    : " + _phone);
        Console.WriteLine("___________________\n");
    }

    // SendEmail method
    public void SendEmail(string title, string body)
    {
        // Implement SendEmail functionality
    }

    // SendFAX method
    public void SendFAX(string title, string body)
    {
        // Implement SendFAX functionality
    }

    // SendSMS method
    public void SendSMS(string title, string body)
    {
        // Implement SendSMS functionality
    }
}
