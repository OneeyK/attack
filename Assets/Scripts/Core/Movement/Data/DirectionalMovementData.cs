using System;
using UnityEngine;

namespace Core.Movement.Data
{
    
    [Serializable]
    public class DirectionalMovementData
    {
       
        //[field: SerializeField] public float HorizontalSpeed { get; private set;  }
        [field: SerializeField] public bool FaceRight { get; private set;  }

     
       // [field: SerializeField] public float VerticalSpeed { get; private set;  }
        [field: SerializeField] public float MinSize { get; private set;  }
        [field: SerializeField] public float MaxSize { get; private set;  }
        [field: SerializeField] public float MaxVerticalPosition { get; private set;  }
        [field: SerializeField] public float MinVerticalPosition { get; private set;  }
    }
}