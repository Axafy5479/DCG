using RX;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Position
{
    /// <summary>
    /// ���ׂẴ|�W�V�����͂��̒��ۃN���X���p������
    /// </summary>
    public abstract class PositionBase : IPosition
    {
        public PositionBase(bool isPlayer)
        {
            IsPlayer = isPlayer;
        }

        protected Subject<ICardInfo> cardMade  = new Subject<ICardInfo>(null);
        public IObservable<ICardInfo> CardMade => cardMade;

        /// <summary>
        /// �v���C���[��Position���ۂ�
        /// </summary>
        public bool IsPlayer { get; }

        /// <summary>
        /// ���̃|�W�V�����ɑ��݂���J�[�h�S��
        /// </summary>
        public ReadOnlyCollection<ICardInfo> Cards =>_cards.ConvertAll(c=>c as ICardInfo).AsReadOnly();
        protected ReadOnlyCollection<ICardMover> cards => _cards.AsReadOnly();
        private List<ICardMover> _cards = new List<ICardMover>();

        public abstract Pos Pos { get; }

        /// <summary>
        /// �J�[�h���ړ�����
        /// </summary>
        /// <typeparam name="T">�ړ���</typeparam>
        /// <param name="index">�ړ���ł̔z�u�ʒu</param>
        /// <param name="gameid">�ړ�����J�[�h��Id</param>
        /// <returns></returns>
        public bool TryMoveTo<T>(int index,int gameid)where T:PositionBase
        {
            //gameid����J�[�h�̃C���^�[�t�F�[�X��T��
            ICardMover card = _cards.Find(c=>c.GameId == gameid);

            //�^�uT�v����ړ���̃C���X�^���X��T��
            PositionBase positionTo = PositionLocator.LI.Resolve<T>(IsPlayer);

            //�ړ���ɃJ�[�h���ǉ��ł����ԂȂ�...
            if(positionTo.CanAdd())
            {
                //�J�[�h�������o�����Ȃ��...
                if(TryRemove(card))
                {
                    //�J�[�h��ǉ�
                    positionTo.Add(index,card);

                    //�J�[�h��Status��Pos��ύX
                    card.ChangePos(positionTo.Pos);

                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// �J�[�h��ǉ��ł����Ԃ��ۂ�
        /// </summary>
        /// <returns></returns>
        public abstract bool CanAdd();

        /// <summary>
        /// �J�[�h��ǉ�����
        /// </summary>
        /// <param name="index"></param>
        /// <param name="card"></param>
        protected virtual void Add(int index, ICardInfo card)
        {
            _cards.Insert(index,card as ICardMover);
        }

        /// <summary>
        /// �J�[�h����������
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        protected virtual bool TryRemove(ICardInfo card)
        {
            return _cards.Remove(card as ICardMover);
        }

        public ICardInfo MakeCardAndAdd(CardBook book,int index)
        {
            ICardInfo cardInfo = book switch
            {
                CardBook_Chara chara => new CardStatus_Chara(chara, Pos, IsPlayer, GameCardID.I.GetId(book.BookId)),
                _=>throw new System.Exception(book.GetType()+"�ɑ�������X�e�[�^�X�����݂��܂���")
            };

            Add(index, cardInfo);

            cardMade.OnNext(cardInfo);

            //PlayableId��ݒ�
            return cardInfo;
        }

        public void OnBeginTurn(bool turn)
        {
            if (turn != IsPlayer)
            {
                foreach (var c in cards)
                {
                    ((CardStatus)c).ChangePlayable(false);
                }
            }
            else
            {
                _onBeginTurn();
            }
        }

        protected virtual void _onBeginTurn() { }
        public virtual void PositionJudge() { }
    }
}
