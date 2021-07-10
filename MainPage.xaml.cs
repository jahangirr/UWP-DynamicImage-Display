using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DoctorList
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        List<MyDoctorList> myDoctorList = new List<MyDoctorList>();
        MyDoctorList myDoctor = new MyDoctorList();

        async void LoadNotePadInfo()
        {
            string text = "";
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///DoctorList.txt"));
            var buffer = await Windows.Storage.FileIO.ReadBufferAsync(file);
            using (var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(buffer))
            {
                text = dataReader.ReadString(buffer.Length);
            }

            string[] totalString = text.Substring(0, text.Length - 1).Split(',');

            for (int i = 0; i < totalString.Length-4; i++)
            {
                if (i % 6.00 == 0.00)
                {
                    myDoctorList.Add(new MyDoctorList()
                    {
                        doctorId = totalString[i + 0],
                        doctorImage = totalString[i + 5],
                        doctorDegree = totalString[i + 3],
                        doctorEmail = totalString[i + 4],
                        doctorAddress = totalString[i + 2],
                        doctorName = totalString[i + 1]


                    });
                }

            }

           foreach(var v in myDoctorList)
            {
                var kk = v;
            }

            myList.ItemsSource = myDoctorList;


        }
        public MainPage()
        {
            this.InitializeComponent();


             LoadNotePadInfo();

          

            //string path = "ms-appx:///Assets/DoctorList.txt";

            ////string path = "DoctorList.txt";

            //List<string> allData = File.ReadAllLines(path).ToList();

            //foreach (var ad in allData)
            //{
            //    if (ad.Trim() != "")
            //        myDoctorList.Add(new MyDoctorList()
            //        {
            //            doctorId = ad.Split(',')[0],
            //            doctorImage = ad.Split(',')[5],
            //            doctorDegree = ad.Split(',')[3],
            //            doctorEmail = ad.Split(',')[4],
            //            doctorAddress = ad.Split(',')[2],
            //            doctorName = ad.Split(',')[1],
            //            // doctorName = 



            //        });
            //}



         
           
        }


        async void LoadImage(string doctorId)
        {

             string url = "ms-appx:///Assets/"+ doctorId.Trim() + ".jpg";

            var rass = RandomAccessStreamReference.CreateFromUri(new Uri(url));
            using (IRandomAccessStream stream = await rass.OpenReadAsync())
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.SetSource(stream);
                docImage.Source =  bitmapImage;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string doctorId = ((Button)sender).Content.ToString();
            //this.Frame.
            LoadImage(doctorId);
            myDoctor = myDoctorList.Where(w => w.doctorId.Trim() == doctorId.Trim()).FirstOrDefault();
            doctorEmail.Text = "Email :" +  myDoctor.doctorEmail;
            doctorDegree.Text = "Degree :" + myDoctor.doctorDegree;

            doctorName.Text = "Name :" + myDoctor.doctorName;
            doctorAddress.Text = "Address :" + myDoctor.doctorAddress;


        }

       

        
    }

   
    
}
