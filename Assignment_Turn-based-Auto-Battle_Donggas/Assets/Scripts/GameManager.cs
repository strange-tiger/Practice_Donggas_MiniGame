using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player[] _players;

    private Coroutine _currentPassTime;
    private bool _gameOver = false;
    private void Awake()
    {
        foreach (Player player in _players)
        {
            player.OnDead -= StopGame;
            player.OnDead += StopGame;

            player.AttackEnd -= ContinueGame;
            player.AttackEnd += ContinueGame;
        }

        _currentPassTime = StartCoroutine(passTime());
    }

    private void OnDisable()
    {
        foreach (Player player in _players)
        {
            player.OnDead -= StopGame;
            player.AttackEnd -= ContinueGame;
        }
    }

    private void StopGame()
    {
        _gameOver = true;
        StopAllCoroutines();
    }

    private void ContinueGame()
    {
        if (_gameOver) { return; }
        _currentPassTime = StartCoroutine(passTime());
    }

    private static readonly WaitForSeconds DELAY_TIME = new WaitForSeconds(1f);
    private IEnumerator passTime()
    {
        while (!_gameOver)
        {
            FillActionGauge();

            yield return DELAY_TIME;
        }
    }

    private void FillActionGauge()
    {
        for (int i = 0; i < _players.Length; ++i)
        {
            _players[i].ActionGauge += _players[i].Speed;

            if (_players[i].ActionGauge < Player.MAX_ACTION)
            {
                continue;
            }

            // ������ �÷��̾ �� ���̱⿡ 0�̳� 1�̳ķ� �Ǻ��Ͽ� �ǰ��� ����������,
            // �÷��̾ �� �������� ���� ID�� ���� ������ ������ ���̴�.
            if (_players[i].SkillAvailable)
            {
                _players[i].UseSkill(_players[i], _players[1 - i]);
            }
            else
            {
                DefaultAttack(_players[i], _players[1 - i]);
            }

            StopAllCoroutines();
            StartCoroutine(_players[i].onAction());

            return;
        }
    }

    private void DefaultAttack(Player attackPlayer, Player defensePlayer)
    {
        defensePlayer.Damaged(attackPlayer.Damage);
    }
}
