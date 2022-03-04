# Hmmm...

So I was experiencing some bugs with the kinematic equations from [[3-3 Progress]], particularly relating to the angle at which you cast when looking up or down. This could have a trigonometric solution where I scale the $v_0$ each axis by the sin or cosine of the angle the camera is at... Either way, I have to think about a way to fix this, or a newer approach...

At least now I know how to use the kinematic equations a lot better! Now I've sorted some of the TransformPoint() use cases in my mind just a bit more. Essentially, the kinematic equations help me calculate 1-dimensional offset points in their respective axes.
These values are the ones I need to apply to the `originTransform.position`. I need to also apply these scaled against the `originTransform.forward` vector so that they happen relative to the forward vector.

A bit of these pains seem to result from me trying to do these kinematic equations in 2D and trying to make that into a 3D point. If I were to involve some trig or something it should be able to work, but I would like to use the TransformPoint().

Alrighty.............................
So I've realized a little something about these equations. Unless I find a way to properly scale them when accounting for vertical rotation and such initially, they aren't useful. It might be better to use something like a more standard parabola instead.
The parabola might be easier to calculate, but would be about the same process to transform and all that. This is unfortunate! I really liked those kinematic equations, they were based in forces that made a lot of sense. I could alter the gravity, initial velocity, and more to have them make sense. Unfortunately, if I don't find a way to fix this vertical rotation problem, I won't have nearly as much fun as I want to...

That said, I have to look into why exactly this phenomenon occurs, why rotation would impact the angle of the throw the way it does. The issue *seems* to be that the kinematic equations are working as intended, it's just that the rotation is actually rotating the ENTIRE equation instead of changing the x and y initial velocities.
I think I predicted this outcome when I realized that the x and y initial velocities (I really should think in terms of z over x here) would have to be scaled with sin() and cos(), but I wasn't sure how exactly. **What I need is to remove whatever is causing the vertical rotation's influence on the whole equation, somehow. After this, I need to manipulate the x and y values based on the rotation of the transform.** I'm fairly sure that's exactly what I'm looking for.

OK I need Jesus. I knew this would continue to be a brain-fryer to solve and I was right, but I know I'll get it done. The main problem here is that I'm having trouble diagnosing the behavior of the effect of the transformed vector on the outcome of the final vector. What I've come to REALIZE is that the transformed vector (or the result of the tranform.TransformPoint) takes rotation into account, and therefore will affect the values according to the rotation. That said, the values that I WANT affected by the rotation, or transformed, are the x and z values. The y value of the equation should remain consistent so I can manipulate the actual kinematic equation that generates the arc later based on the sin() cos() stuff.

```CS
var mixedVectorOnlyXZ = new Vector3(
transformedOffsetVector3.x,
localOffsetVector3.y + transform.position.y,
transformedOffsetVector3.z);
```

This equation allows me to have my sweet,  sweet consistent y values that are unaffected by the rotation. These NEED to have their kinematic equations manipulated INSTEAD of having their points transformed.
As for the x and z values, those are impacted quite heavily by the Z-rotation of the parent gameObject, which is proving to be troublesome. On their own, however, they obey the y rotation well which is what I want. In fact, I really ONLY want y rotation to be accounted for. Nothing else matters in my opinion. Could I use trigonometry for this? Probably I should. How? I don't know yet.

What I ultimately need to figure out is how to ONLY figure out the offsets of these points rotated around the y axis rotation. IN THEORY that sounds like me defining the x and z values as something like `transform.position + (sin/cos(yRotationEulers) * distanceScalarValue)`... Hmmmmmm.

I DID IT! I never like feeling like I'm talking myself up, but man I've got a brain on me. Not even for being particularly smart or whatever, but for perservering and figuring this all out.

What needed to happen was the following:
```CS
var mixedVectorOnlyXZ = new Vector3(
transform.position.x + (Mathf.Sin(Mathf.Deg2Rad * yRotationEulers) * localOffsetVector3.z),
transform.position.y + localOffsetVector3.y,
transform.position.z + (Mathf.Cos(Mathf.Deg2Rad * yRotationEulers) * localOffsetVector3.z));
```
It looks like a bunch of crap, but it's essentially me getting the x and y positions using ONLY the y rotation from the transform. I'm doing this using sine and cosine, but I needed to convert degrees to radians to properly get the rotation as I need it to rotate. And thank goodness!

This rocks, a lot. Using the Gizmos drawing approach that I've implemented, I've moved the variables and equations up to the `FishingRodGearContext` class instead of the casting class. This way, I can store the variables related to a cast if I need to, and especially so I can tweak the kinematic equation variables to manipulate the actual cast equation.

Now that the rotational issue was fixed, I can proceed to changing the cast variables based on the rotation angle.

ALRIGHTY so I think I've finally got a decent set of variables on my hands. I'll store them here just in case:
```CS
// Horizontal Equation
public float x_v0 = 6.5f; // Scaled by NormalizedCastPower!
public float x_v1 = 0f;

// Vertical Equation
public float y_v0 = 5.5f; // Scaled by NormalizedCastPower
public float y_a = -3.5f;
```

Goodness gracious, that was kind of fun. I'm glad I'm somewhat happy with my result now. Now, however, I have to decide how I'll implement this with the casting and things like that. I think I'll keep the equations within the `FishingGearContext` class and make them available from it.

Within the aiming class now, I think I'll generate the test points and place some gameObject that indicates the landing position of the bobber with everything.

---

I've been writing too long without documenting. Essentially, I've ran into my next snag which is calculating all my test points and checking for collision again. Something is going wrong as I do this, however.
I'm thinking I need to make a function that does what my old collision point testing function did, which was iteratively test each point AS SOON AS I made the point. This worked well for the dynamically-sized list, but I wanted to work in arrays because that seemed better to me. Now, however, my current approach isn't working for me... How troublesome.

Testing my old function, it works, but I don't like the idea of clearing and rewriting to a list EVERY SINGLE frame in Update. Just doesn't sit with me well unless I do it in a coroutine or something...

Gah I just realized why, I'm generating test points until collision and all that, but when I place my bobberPreview at the area, it has a collider on it so it slides all the way up the arc, LOLL! How foolish of me.

Good lord that was stupid, lol... That's unfortunate. At least I've figured it out! Now, I know what's going on, which I'm really happy with!