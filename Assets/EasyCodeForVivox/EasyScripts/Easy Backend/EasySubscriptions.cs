using System;
using UnityEngine;
using VivoxUnity;

namespace EasyCodeForVivox
{
    public class EasySubscriptions
    {
        public static event Action<AccountId> SubscriptionAddAllowed;
        public static event Action<AccountId> SubscriptionRemoveAllowed;

        public static event Action<AccountId> SubscriptionAddBlocked;
        public static event Action<AccountId> SubscriptionRemoveBlocked;

        public static event Action<AccountId> SubscriptionAddPresence;
        public static event Action<AccountId> SubscriptionRemovePresence;

        public static event Action<ValueEventArg<AccountId, IPresenceSubscription>> SubscriptionUpdatePresence;

        public static event Action<AccountId> SubscriptionIncomingRequest;


        public void Subscribe(ILoginSession loginSession)
        {
            loginSession.AllowedSubscriptions.AfterKeyAdded += OnAddAllowedSubscription;
            loginSession.AllowedSubscriptions.BeforeKeyRemoved += OnRemoveAllowedSubscription;

            loginSession.BlockedSubscriptions.AfterKeyAdded += OnAddBlockedSubscription;
            loginSession.BlockedSubscriptions.BeforeKeyRemoved += OnRemoveBlockedSubscription;

            loginSession.PresenceSubscriptions.AfterKeyAdded += OnAddPresenceSubscription;
            loginSession.PresenceSubscriptions.BeforeKeyRemoved += OnRemovePresenceSubscription;
            loginSession.PresenceSubscriptions.AfterValueUpdated += OnUpdatedPresenceSubscription;

            loginSession.IncomingSubscriptionRequests.AfterItemAdded += OnIncomingSubscriptionRequests;
        }

        public void Unsubscribe(ILoginSession loginSession)
        {
            loginSession.AllowedSubscriptions.AfterKeyAdded -= OnAddAllowedSubscription;
            loginSession.AllowedSubscriptions.BeforeKeyRemoved -= OnRemoveAllowedSubscription;

            loginSession.BlockedSubscriptions.AfterKeyAdded -= OnAddBlockedSubscription;
            loginSession.BlockedSubscriptions.BeforeKeyRemoved -= OnRemoveBlockedSubscription;

            loginSession.PresenceSubscriptions.AfterKeyAdded -= OnAddPresenceSubscription;
            loginSession.PresenceSubscriptions.BeforeKeyRemoved -= OnRemovePresenceSubscription;
            loginSession.PresenceSubscriptions.AfterValueUpdated -= OnUpdatedPresenceSubscription;

            loginSession.IncomingSubscriptionRequests.AfterItemAdded -= OnIncomingSubscriptionRequests;
        }



        #region Subscription / Presence Events


        private void OnAddAllowedSubscription(AccountId accountId)
        {
            if (accountId != null)
            {
                SubscriptionAddAllowed?.Invoke(accountId);
            }
        }

        private void OnRemoveAllowedSubscription(AccountId accountId)
        {
            if (accountId != null)
            {
                SubscriptionRemoveAllowed?.Invoke(accountId);
            }
        }

        private void OnAddPresenceSubscription(AccountId accountId)
        {
            if (accountId != null)
            {
                SubscriptionAddPresence?.Invoke(accountId);
            }
        }

        private void OnRemovePresenceSubscription(AccountId accountId)
        {
            if (accountId != null)
            {
                SubscriptionRemovePresence?.Invoke(accountId);
            }
        }

        private void OnUpdatePresenceSubscription(ValueEventArg<AccountId, IPresenceSubscription> presence)
        {
            if (presence != null)
            {
                SubscriptionUpdatePresence?.Invoke(presence);
            }
        }

        private void OnAddBlockedSubscription(AccountId accountId)
        {
            if (accountId != null)
            {
                SubscriptionAddBlocked?.Invoke(accountId);
            }
        }

        private void OnRemoveBlockedSubscription(AccountId accountId)
        {
            if (accountId != null)
            {
                SubscriptionRemoveBlocked?.Invoke(accountId);
            }
        }
        

        private void OnIncomingSubscription(AccountId subRequest)
        {
            if (subRequest != null)
            {
                SubscriptionIncomingRequest?.Invoke(subRequest);
            }
        }


        #endregion



        #region Subscriptions / Presence Methods


        public void AddAllowedSubscription(string userSIP, ILoginSession loginSession)
        {
            loginSession.BeginAddAllowedSubscription(new AccountId(userSIP), ar =>
            {
                try
                {
                    loginSession.EndAddAllowedSubscription(ar);
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            });
        }

        public void AddBlockedSubscription(string userSIP, ILoginSession loginSession)
        {
            loginSession.BeginAddBlockedSubscription(new AccountId(userSIP), ar =>
            {
                try
                {
                    loginSession.EndAddBlockedSubscription(ar);
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            });
        }

        public void AddAllowPresence(string userSIP, ILoginSession loginSession)
        {
            loginSession.BeginAddPresenceSubscription(new AccountId(userSIP), ar =>
            {
                try
                {
                    loginSession.EndAddPresenceSubscription(ar);
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            });
        }


        public void RemoveAllowedSubscription(string userSIP, ILoginSession loginSession)
        {
            loginSession.BeginRemoveAllowedSubscription(new AccountId(userSIP), ar =>
            {
                try
                {
                    loginSession.EndRemoveAllowedSubscription(ar);
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            });
        }

        public void RemoveBlockedSubscription(string userSIP, ILoginSession loginSession)
        {
            loginSession.BeginRemoveBlockedSubscription(new AccountId(userSIP), ar =>
            {
                try
                {
                    loginSession.EndRemoveBlockedSubscription(ar);
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            });
        }

        public void RemoveAllowedPresence(string userSIP, ILoginSession loginSession)
        {
            loginSession.BeginRemovePresenceSubscription(new AccountId(userSIP), ar =>
            {
                try
                {
                    loginSession.EndRemovePresenceSubscription(ar);
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            });
        }


        #endregion



        #region Subscription / Presence Callbacks


        private void OnAddAllowedSubscription(object sender, KeyEventArg<AccountId> keyArgs)
        {
            var source = (IReadOnlyHashSet<AccountId>)sender;

            OnAddAllowedSubscription(keyArgs.Key);
        }

        private void OnRemoveAllowedSubscription(object sender, KeyEventArg<AccountId> keyArgs)
        {
            var source = (IReadOnlyHashSet<AccountId>)sender;

            OnRemoveAllowedSubscription(keyArgs.Key);
        }

        private void OnAddPresenceSubscription(object sender, KeyEventArg<AccountId> keyArgs)
        {
            var source = (VivoxUnity.IReadOnlyDictionary<AccountId, IPresenceSubscription>)sender;

            OnAddPresenceSubscription(keyArgs.Key);
        }

        private void OnRemovePresenceSubscription(object sender, KeyEventArg<AccountId> keyArgs)
        {
            var source = (VivoxUnity.IReadOnlyDictionary<AccountId, IPresenceSubscription>)sender;

            OnRemovePresenceSubscription(keyArgs.Key);
        }

        private void OnUpdatedPresenceSubscription(object sender, ValueEventArg<AccountId, IPresenceSubscription> keyArgs)
        {
            var source = (VivoxUnity.IReadOnlyDictionary<AccountId, IPresenceSubscription>)sender;

            OnUpdatePresenceSubscription(keyArgs);
        }

        private void OnAddBlockedSubscription(object sender, KeyEventArg<AccountId> keyArgs)
        {
            var source = (IReadOnlyHashSet<AccountId>)sender;

            OnAddBlockedSubscription(keyArgs.Key);
        }

        private void OnRemoveBlockedSubscription(object sender, KeyEventArg<AccountId> keyArgs)
        {
            var source = (IReadOnlyHashSet<AccountId>)sender;
            
            OnRemoveBlockedSubscription(keyArgs.Key);
        }

        private void OnIncomingSubscriptionRequests(object sender, QueueItemAddedEventArgs<AccountId> subRequests)
        {
            var source = (IReadOnlyQueue<AccountId>)sender;
            while (source.Count > 0)
            {
                // todo check if it works
                var request = source.Dequeue();
                Debug.Log($"Incoming subscription request from - {request.Name}");
                Debug.Log($"Incoming subscription request from - {request.DisplayName}");
                OnIncomingSubscription(request);
            }
        }


        #endregion
    }
}