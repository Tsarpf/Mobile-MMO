RahatPois
=========

In finland we call it rahasampo



Main features: 
- Client made with Unity3D, 3D graphics with camera locked to being isometric.
    * first only for mobile (at least android, hopefully ios too)
    * when unity 5 comes with desktop browser support without unity plugin, then support that too.
    * minigames, at first only easy ones like rock-paper-scissors and bejeweled.
    * minimalistic user interactions (f.ex only four major directions to move in) so coding the server is easier and less error prone.
    * ...?
- NodeJS(?) backend to minimize coding and increase ~scalability~
  * Normal authing (st|fl)uff
  * Easily re-establishable socket connections so frequent disconnects because of mobile networks cause minimal problems in gameplay
  * Authoritative, clients can only request for actions, server responds with what actually happened (if anything)
  * ...?
