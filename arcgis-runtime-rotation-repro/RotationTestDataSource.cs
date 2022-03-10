using Esri.ArcGISRuntime.Location;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace arcgis_runtime_rotation_repro
{
    public class RotationTestDataSource : LocationDataSource, System.ComponentModel.INotifyPropertyChanged
    {
        public RotationTestDataSource(double latitude, double longitude)
        {
            MapPoint = new Esri.ArcGISRuntime.Geometry.MapPoint(longitude, latitude, Esri.ArcGISRuntime.Geometry.SpatialReferences.Wgs84);

        }

        private readonly Esri.ArcGISRuntime.Geometry.MapPoint MapPoint;
        private double _heading;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public double Heading
        {
            get
            {
                return _heading;
            }
            set
            {
                _heading = value;
                OnPropertyChanged();
                this.UpdateHeading(_heading);
                this.UpdateLocation(new Location(DateTimeOffset.Now, MapPoint, double.NaN, double.NaN, 10, _heading, false));

            }
        }

        protected override Task OnStartAsync()
        {
            return Task.CompletedTask;
        }

        protected override Task OnStopAsync()
        {
            return Task.CompletedTask;
        }
    }
}
