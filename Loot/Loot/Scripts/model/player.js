var Player = function(build)
{
    this.opponent;
    this.status;
    this.name = build.name;
    this.life = build.maxLife;
    this.maxLife = build.maxLife;
    this.meter = build.maxMeter;
    this.maxMeter = build.maxMeter;
    this.meterOnHit = build.meterOnHit;
    this.mitigationPercent = calculateMitigation(build);
    this.lifePerSecond = build.lifePerSecond;
    this.lifeOnHit = build.lifeOnHit;
    
    build.primaryAttack.attacksPerSecond = build.attacksPerSecond;
    build.secondaryAttack.attacksPerSecond = build.attacksPerSecond;

    this.primaryAttack = new Attack(this, build.primaryAttack);
    this.secondaryAttack = new Attack(this, build.secondaryAttack);

    this.healer = new Healer(this, build);

    this.attack = function () {

        if (!this.isAttacking()) {

            if (this.secondaryAttack.canAttack()) {

                this.secondaryAttack.attack();
            }
            else if(this.primaryAttack.canAttack()){

                this.primaryAttack.attack();
            }
        }
    }

    this.takeDamage = function(damage) {

        if (this.isAlive()) {

            this.life -= damage * this.mitigationPercent;
        }

        if (!this.isAlive()) {

            this.life = 0;
            this.status = "Dead"
        }

        
    }

    this.heal = function (durationInMillis) {

        this.healer.healForDuration(durationInMillis)
    }

    this.healOnHit = function () {
    
        this.healer.healForHit();
    }

    this.isAlive = function () {

        return this.life > 0;
    }

    this.isAttacking = function () {

        return this.primaryAttack.isAttacking || this.secondaryAttack.isAttacking;
    }
}