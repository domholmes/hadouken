var Attack = function (attackingPlayer, attackStats) {

    this.player = attackingPlayer;
    this.stats = attackStats;
    this.attackStartup = 1000 / attackStats.attacksPerSecond;
    this.isAttacking = false;

    this.canAttack = function () {

        return (!this.isAttacking && this.player.meter >= attackStats.meterCost);
    }

    this.attack = function(){

        var self = this;

        self.isAttacking = true;

        window.setTimeout(function () {

            if (self.player.isAlive()) {

                self.player.meter -= attackStats.meterCost;
                self.player.meter += attackStats.meterOnHit;
                if (self.player.meter < 0) {
                    self.player.meter = 0
                }

                self.player.opponent.takeDamage(self.stats.damage);
                self.player.healOnHit();
            }

            self.isAttacking = false;
        }, self.attackStartup);
    }
}