﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
         using UnityEngine;
using ZeroFormatter;
         
namespace NetworkLibrary.NetworkLibrary.Udp.ServerToPlayer.PositionMessages
{
    [ZeroFormattable]
    public struct TestPositionsMessage 
    {
        [Index(0)] public Dictionary<ushort, ViewTransform>EntitiesInfo { get; set; }
        [Index(1)] public Dictionary<ushort, ushort>__RadiusInfo { get; set; }
        
        public TestPositionsMessage(Dictionary<ushort, ViewTransform>entitiesInfo,
            Dictionary<ushort, ushort> radiusInfo)
        {
            EntitiesInfo = entitiesInfo;
            __RadiusInfo = radiusInfo;
        }
    }
    
    [ZeroFormattable]
    public class PositionsMessage : ITypedMessage
    {
        [Index(0)] public virtual Dictionary<ushort, ViewTransform> EntitiesInfo { get; set; }
        //TODO: перенести в UDP с подтверждением
        [Index(1)] public virtual ushort PlayerEntityId { get; set; }
        [Index(2)] public virtual Dictionary<ushort, ushort> RadiusInfo { get; set; }

        [IgnoreFormat]
        public virtual Dictionary<ushort, float> FloatRadiusInfo =>
            RadiusInfo.ToDictionary(pair => pair.Key, pair => Mathf.HalfToFloat(pair.Value));
        
        public PositionsMessage()
        {
        }

        public MessageType GetMessageType() => MessageType.Positions;
    }
    
    [ZeroFormattable]
    public struct Vector2
    {
        [Index(0)] public ushort __x;
        [Index(1)] public ushort __y;
        
        [IgnoreFormat] public float X => Mathf.HalfToFloat(__x);
        [IgnoreFormat] public float Y => Mathf.HalfToFloat(__y);
        
        public Vector2(ushort x, ushort y)
        {
            __x = x;
            __y = y;
        }
        
        public Vector2(float x, float y)
        {
            __x = Mathf.FloatToHalf(x);
            __y = Mathf.FloatToHalf(y);
        }

        public override string ToString()
        {
            return $"({X}; {Y})";
        }

#if UNITY_5_3_OR_NEWER
        public static implicit operator UnityEngine.Vector2(Vector2 vector) => new UnityEngine.Vector2(vector.X, vector.Y);
        public static implicit operator Vector2(UnityEngine.Vector2 vector) => new Vector2(vector.x, vector.y);
#endif
    }
    
    /// <summary>
    /// 7 байтов
    /// </summary>
    [ZeroFormattable]
    public struct ViewTransform
    {
        [Index(0)] public ushort __x;
        [Index(1)] public ushort __y;
        [Index(2)] public ushort __angle;
        [Index(3)] public ViewTypeId typeId;

        [IgnoreFormat] public float X => Mathf.HalfToFloat(__x);
        [IgnoreFormat] public float Y => Mathf.HalfToFloat(__y);
        [IgnoreFormat] public float Angle
        {
            get => Mathf.HalfToFloat(__angle);
            set => __angle = Mathf.FloatToHalf(value);
        }

        public ViewTransform(ushort x, ushort y, ushort angle, ViewTypeId typeId)
        {
            __x = x;
            __y = y;
            __angle = angle;
            this.typeId = typeId;
        }
        
        public ViewTransform(float x, float y, float angle, ViewTypeId typeId)
        {
            __x = Mathf.FloatToHalf(x);
            __y = Mathf.FloatToHalf(y);
            __angle = Mathf.FloatToHalf(angle);
            this.typeId = typeId;
        }

        public ViewTransform(float x, float y, ViewTypeId typeId) : this(x, y, 0f, typeId)
        { }

        public ViewTransform(Vector2 position, ViewTypeId typeId) : this(position.X, position.Y, typeId)
        { }

        public ViewTransform(Vector2 position, float angle, ViewTypeId typeId) : this(position.X, position.Y, angle, typeId)
        { }

        public Vector2 GetPosition() => new Vector2(X, Y);

        public static ViewTransform operator +(ViewTransform t1, ViewTransform t2)
        {
            if(t1.typeId != t2.typeId) throw new NotSupportedException(nameof(typeId) + " не совпали!");
            return new ViewTransform(t1.X + t2.X, t1.Y + t2.Y, t1.Angle + t2.Angle, t2.typeId);
        }

        public static ViewTransform operator -(ViewTransform t1, ViewTransform t2)
        {
            if (t1.typeId != t2.typeId) throw new NotSupportedException(nameof(typeId) + " не совпали!");
            return new ViewTransform(t1.X - t2.X, t1.Y - t2.Y, t1.Angle - t2.Angle, t2.typeId);
        }

        public static ViewTransform operator *(ViewTransform t, float k)
        {
            return new ViewTransform(t.X * k, t.Y * k, t.Angle * k, t.typeId);
        }

        public static ViewTransform operator *(float k, ViewTransform t) => t * k;

        public static ViewTransform operator /(ViewTransform t, float k)
        {
            return new ViewTransform(t.X / k, t.Y / k, t.Angle / k, t.typeId);
        }
    }
    
    [ZeroFormattable]
    public class TestMessageClass1
    {
        [Index(0)] public virtual int SomeNumber { get; set; }

        public TestMessageClass1()
        {
            
        }
    }
    
    [ZeroFormattable]
    public struct TestMessageStruct1
    {
        [Index(0)] public int SomeNumber;

        public TestMessageStruct1(int someNumber)
        {
            SomeNumber = someNumber;
        }
    }
}