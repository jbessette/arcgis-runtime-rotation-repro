using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.UI;
using System;
using System.Windows;

namespace arcgis_runtime_rotation_repro
{
    public partial class MainWindow
    {

        public MainWindow()
        {
            InitializeComponent();
            Initialize();

            DataContext = this;

            LocationDataSource = new RotationTestDataSource(33.826765, -116.539478);
            LocationDataSource.HeadingChanged += async (s, e) =>
            {
                await MyMapView.SetViewpointRotationAsync(e);
            };
            LocationDataSource.StartAsync();
        }

        public RotationTestDataSource LocationDataSource { get; set; }

        private void Initialize()
        {
            Esri.ArcGISRuntime.ArcGISRuntimeEnvironment.ApiKey = Properties.Settings.Default.ApiKey;
            Esri.ArcGISRuntime.ArcGISRuntimeEnvironment.Initialize();

            // Create new Map with basemap
            Map myMap = new Map(BasemapStyle.ArcGISImagery);
            myMap.LoadStatusChanged += (s, e) =>
            {
                if (e.Status == Esri.ArcGISRuntime.LoadStatus.Loaded)
                {
                    Dispatcher.Invoke(() => {
                        MyMapView.LocationDisplay.DataSource = LocationDataSource;
                        MyMapView.LocationDisplay.AutoPanMode = LocationDisplayAutoPanMode.Recenter;
                        MyMapView.LocationDisplay.IsEnabled = true;

                        LocationDataSource.Heading = 0;
                    });
                }
            };

            // Provide used Map to the MapView
            MyMapView.Map = myMap;

        }


    }
}
