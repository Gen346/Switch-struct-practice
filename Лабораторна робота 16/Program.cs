using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

struct Cubes
{
    public string? name;
    public string? length;
    public string? color;
    public string? material;
}

class Program
{
    static List<Cubes> cubes = new List<Cubes>();
    static string filePath = "CubesData.txt"; 

    static void Main(string[] args)
    {
        while (true)
        {
            LoadData(); 

            Console.WriteLine("What do you want to do? Press a button: ");
            Console.WriteLine("1 - Create a new cube");
            Console.WriteLine("2 - Edit a cube");
            Console.WriteLine("3 - Delete a cube");
            Console.WriteLine("4 - Exit");
            Console.WriteLine("5 - See all cubes");
            Console.WriteLine("6 - Cubes made from wood");
            Console.WriteLine("7 - Cubes made from metall");
            Console.WriteLine("8 - Clear file");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        AddCube();
                        Output();
                        break;
                    case 2:
                        Console.Clear();
                        Output();
                        EditCube();
                        break;
                    case 3:
                        Console.Clear();
                        DeleteCube();
                        Output();
                        break;
                    case 4:
                        SaveData();
                        Environment.Exit(0);
                        break;
                    case 5:
                        Output();
                        break;
                    case 6:
                        Console.Clear();
                        WoodCube();
                        break;
                    case 7:
                        MetallCube();
                        break;
                    case 8:
                        ClearFile();
                        break;
                    default:
                        SaveData(); 
                        break;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid input. Please enter a number.");
                Console.WriteLine();
                Thread.Sleep(1000);
                Console.Clear();
            }
        }
    }

    static void Output()
    {
        foreach (var cube in cubes)
        {
            Console.WriteLine($"Name: {cube.name}");
            Console.WriteLine($"Length: {cube.length}");
            Console.WriteLine($"Color: {cube.color}");
            Console.WriteLine($"Material: {cube.material}");
            Console.WriteLine();
        }
    }

    static void DeleteCube()
    {
        Console.WriteLine("Enter the name of the cube to delete: ");
        string? targetName = Console.ReadLine();

        Cubes targetCube = cubes.Find(cube => cube.name == targetName);

        if (targetCube.name != null)
        {
            cubes.Remove(targetCube);
            Console.Clear();
            Console.WriteLine("Cube deleted successfully!");
            Thread.Sleep(1000);
        }
        else
        {
            Console.WriteLine("Cube does not exist.");
            Console.Clear();
            Console.WriteLine();
            Thread.Sleep(1000);
            Console.Clear();
        }
        SaveData();
    }

    static void AddCube()
    {
        Console.WriteLine("Input the name of the cube: ");
        string? name = Console.ReadLine();

        Console.WriteLine("Input the length of the cube: ");
        string? length = Console.ReadLine();

        Console.WriteLine("Input the color of the cube: ");
        string? color = Console.ReadLine();

        Console.WriteLine("Input the material of the cube: ");
        string? material = Console.ReadLine();

        Cubes newCube = new Cubes { name = name, length = length, color = color, material = material };

        cubes.Add(newCube); 
        Console.Clear();
        Console.WriteLine("Cube added successfully!");
        Console.WriteLine();
        Thread.Sleep(1000);
        Console.Clear();
        SaveData(); 
    }

    static void EditCube()
    {
        Console.WriteLine("Enter the name of the cube to edit: ");
        string? targetName = Console.ReadLine();

        Cubes targetCube = cubes.Find(cube => cube.name == targetName);

        if (targetCube.name != null)
        {
            Console.WriteLine("Enter the new name of the cube: ");
            targetCube.name = Console.ReadLine();

            Console.WriteLine("Enter the new length of the cube: ");
            targetCube.length = Console.ReadLine();

            Console.WriteLine("Enter the new color of the cube: ");
            targetCube.color = Console.ReadLine();

            Console.WriteLine("Enter the new material of the cube: ");
            targetCube.material = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Cube edited successfully!");
            Console.WriteLine();
            Thread.Sleep(1000);
            Console.Clear();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Cube not found.");
            Console.WriteLine();
            Thread.Sleep(1000);
            Console.Clear();
        }

        SaveData(); 
    }

    static void SaveData()
    {
        using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        using (StreamWriter sw = new StreamWriter(fs))
        {
            foreach (var cube in cubes)
            {
                sw.WriteLine($"{cube.name},{cube.length},{cube.color},{cube.material}");
            }
        }
    }

    static void LoadData()
    {
        cubes.Clear(); 
        if (File.Exists(filePath))
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (StreamReader sr = new StreamReader(fs))
            {
                while (!sr.EndOfStream)
                {
                    string? line = sr.ReadLine();
                    string[] parts = line.Split(',');

                    Cubes loadedCube = new Cubes
                    {
                        name = parts[0],
                        length = parts[1],
                        color = parts[2],
                        material = parts[3]
                    };

                    cubes.Add(loadedCube);
                }
            }
        }
    }

    static void WoodCube()
    {
        string? targetMaterial = "Wood";
        string? targetLength = "3";

        List<Cubes> matchingCubes = cubes.FindAll(cube => cube.material == targetMaterial && cube.length == targetLength);

        if (matchingCubes.Count > 0)
        {
            Console.Clear();
            Console.WriteLine($"Wooden cubes with length {targetLength}:");
            Console.WriteLine($"Total count: {matchingCubes.Count}");
            Console.WriteLine();
            Thread.Sleep(1000);
            Console.Clear();
        }
        else
        {
            Console.Clear();
            Console.WriteLine($"No cubes found with material {targetMaterial} and length {targetLength}");
            Console.WriteLine();
            Thread.Sleep(1000);
            Console.Clear();
        }
    }

    static void ClearFile()
    {
        File.WriteAllText(filePath, string.Empty);
        Console.Clear();
        Console.WriteLine("File cleared successfully!");
        Console.WriteLine();
        Thread.Sleep(1000);
        Console.Clear();
    }

    static void MetallCube()
    {
        string? targetMaterial = "Metal";
        double minLength = 5.0;

        int count = cubes.Count(cube => cube.material == targetMaterial && double.Parse(cube.length) > minLength);

        if (count > 0)
        {
            Console.Clear();
            Console.WriteLine($"Number of metal cubes with length greater than {minLength} cm: {count}");
            Console.WriteLine();
            Thread.Sleep(1000);
            Console.Clear();
        }
        else
        {
            Console.Clear();
            Console.WriteLine($"No metal cubes found with length greater than {minLength} cm");
            Console.WriteLine();
            Thread.Sleep(1000);
            Console.Clear();
        }
    }
}
