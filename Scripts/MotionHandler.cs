using System;

public class MotionHandler {

    /* 
        Handles all motion related fields and manipulations, with all abstractions from position
    */
    
    public PointF position, velocity, acceleration;

    public MotionHandler () {
       init(new PointF(0f,0f), new PointF(0f,0f), new PointF(0f,0f));
    }

    /* When only position is known, assume object is motionless */
    public MotionHandler (PointF position) {
        init(position, new PointF(0f,0f), new PointF(0f,0f));
    }

    public void init(PointF position, PointF velocity, PointF acceleration) {
        this.position = position;
        this.velocity = velocity;
        this.acceleration = acceleration;
    }

    public override string ToString () {
        return (
            "Position(" + position.ToString() + ")\n" + 
            "Velocity(" + velocity.ToString() + ")\n" +
            "Acceleration(" + acceleration.ToString() + ")\n"
        );
    }
}
