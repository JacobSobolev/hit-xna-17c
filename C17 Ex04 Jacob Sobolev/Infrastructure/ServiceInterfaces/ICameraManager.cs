using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Infrastructure.ServiceInterfaces
{
    public interface ICameraManager
    { 
        void setCameraSettings();
        void setCameraState();
        Matrix CameraSettings { get; }
        Matrix CameraState { get; }
        Vector3 CameraLooksAt { get; set; }
        Vector3 CameraLocation { get; set; }
        Vector3 CameraUpDirection { get; set; }
        float NearPlaneDistance { get; }
        float FarPlaneDistance { get; }
        float ViewAngle { get; }
    }
}
