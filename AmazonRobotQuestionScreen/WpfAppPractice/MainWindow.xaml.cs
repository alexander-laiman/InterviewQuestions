using System.IO.Packaging;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppPractice;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private Warehouse warehouse;
private Canvas warehouseCanvas;

public MainWindow()
{
    InitializeComponent();
    warehouse = new Warehouse();
    SetupUI();
}

private void SetupUI()
{
    Title = "Warehouse Storage Simulator";
    Width = 500;
    Height = 400;

    var mainPanel = new StackPanel();
    warehouseCanvas = new Canvas { Width = 450, Height = 200, Background = Brushes.LightGray };
    Button addSmallPackage = new Button { Content = "Add Small Package" };
    Button addMediumPackage = new Button { Content = "Add Medium Package" };
    Button addLargePackage = new Button { Content = "Add Large Package" };

    addSmallPackage.Click += (s, e) => AddPackage(Size.Small);
    addMediumPackage.Click += (s, e) => AddPackage(Size.Medium);
    addLargePackage.Click += (s, e) => AddPackage(Size.Large);

    mainPanel.Children.Add(addSmallPackage);
    mainPanel.Children.Add(addMediumPackage);
    mainPanel.Children.Add(addLargePackage);
    mainPanel.Children.Add(warehouseCanvas);

    Content = mainPanel;
    UpdateStorageDisplay();
}

private void AddPackage(Size size)
{
    warehouse.AddPackages(new List<Package> { new Package(size) });
    UpdateStorageDisplay();
}

    private void UpdateStorageDisplay()
    {
        warehouseCanvas.Children.Clear();
        foreach (var unitList in warehouse.AvailableStorage.Values)
        {
            foreach (var unit in unitList)
            {
                Rectangle rect = new Rectangle
                {
                    Width = unit.UnitSize == Size.Small ? 40 : unit.UnitSize == Size.Medium ? 60 : 80,
                    Height = 40,
                    Fill = unit.IsOccupied ? Brushes.Red : Brushes.Green,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2
                };
                Canvas.SetLeft(rect, unit.Position.X);
                Canvas.SetTop(rect, unit.Position.Y);
                warehouseCanvas.Children.Add(rect);
            }
        }
    }
}
