using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    // A classe NPCVelocity é uma subclasse de NPC que substitui o método MoveNPC para mover o NPC com base na velocidade.
    public class NPCVelocity : NPC
    {
        public override void MoveNPC()
        {
            rb.velocity = direction * data.speed;
        }
    }
}