namespace QuotesApp;

public partial class MainPage : ContentPage
{
    List<string> list = new List<string>();
    public MainPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadMauiAsset();
    }
    async Task LoadMauiAsset()
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync("quotes.txt");
        using var reader = new StreamReader(stream);

        while (reader.Peek() != -1)
        {
            list.Add(reader.ReadLine());
        }
    }

    Random random = new Random();
    private void btnGenerateQuote_Clicked(object sender, EventArgs e)
    {
        var startColor = System.Drawing.Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
        var endColor = System.Drawing.Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));

        var colors = ColorUtility.ColorControls.GetColorGradient(startColor, endColor, 6);

        float stopoOffset = 0.0f;
        var stops = new GradientStopCollection();
        foreach (var c in colors)
        {
            stops.Add(new GradientStop(Color.FromArgb(c.Name), stopoOffset));
            stopoOffset += 0.2f;
        }
        var gradiant = new LinearGradientBrush(stops, new Point(0, 0), new Point(1, 1));
        Background.Background = gradiant;

        int index = random.Next(list.Count);
        quote.Text = list[index];
    }
}

