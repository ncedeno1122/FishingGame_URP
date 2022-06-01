# 6-1 Work
Alright, I'm BACK and I'm ready to ROLL! I'm so excited to resume my work from [[5-24 Work]], I got a lot of awesome things done then.

I left off after getting my old casting state system working with the new, non-kinematic casting. This rocks, but it's not fully implemented just yet. There are certain things missing, to say the least, and I just have to determine what the ones I want to tackle are.

One quickfix I just did was having my line renderer loop and draw in LateUpdate for the MAX smoothness to the bobber. It's very satisfying, for whatever that means anymore. I wonder if I should commit this change. Usually, I do them by milestone or something, but I'm not sure how often I want to commit changes if I'm the only one adding assets. I'll think about that later though.

Now, I've got to keep managing my Rigidbody's parent and kinematic stuff as I cast it.

But GRRRrrr, there's something weird about this. I wanted to try again with a parent-based approach to take in the kinematic bobber, but it's still changing things weirdly like the scale. Time to use my Joints, then.

----

RRAAAA, I've broken free from some of this stuff. I'm learning more and more about Rigidbodies every day (for better and for worse, LOL!). It totally makes sense that Kinematic Rigidbodies cannot be moved by physics or stuff like that, I just wasn't sure if joints allowed for that. As I have it now, there is a joint that I'm creating dynamically because I wasn't sure if the then-Kinematic Rigidbody wasn't moving because a FixedJoint was locking it to some world position or something. Now, I'll try maintaining the FixedJoint.

I think maintaining the FixedJoint as a reference without a connectedbody is connecting it to the world?? 

---

So what I was experiencign was the FixedJoint causing some unintended issue without a connecting body. Thus, I elected just to dynamically create and hook up the joint when I need it instead of deal with that hassle. As well, I manipulated the connected anchor so the bobber consistently sits at a nice offset. It works as intended so far, but there's more I have to tackle. 

For example, I 