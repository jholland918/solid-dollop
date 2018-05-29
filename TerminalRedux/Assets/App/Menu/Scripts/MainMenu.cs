using Assets.App.Models;
using MarkLight;
using MarkLight.Views.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

// TODO: Create game creation menus
namespace Assets.App.Menu.Scripts
{
    public class MainMenu : UIView
    {
        public ViewSwitcher ContentViewSwitcher;
        private Stack<string> ViewSwitcherHistory = new Stack<string>(new[] { "Start" });

        public ComboBox SearchGameType;
        public string searchGameType;

        public ComboBox CreateGameType;
        public string createGameType;

        public List LobbyList;
        public ObservableList<Lobby> LobbyItems;

        public List ProfileList;
        public ObservableList<Profile> ProfileItems;

        public List ArsenalList;
        public ObservableList<Arsenal> ArsenalItems;

        public List CharacterList;
        public ObservableList<Character> CharacterItems;

        public List LevelList;
        public ObservableList<Level> LevelItems;

        public override void Initialize()
        {
            base.Initialize();
            LobbyItems = new ObservableList<Lobby>();
            ProfileItems = new ObservableList<Profile>();
            LevelItems = new ObservableList<Level>();
            CharacterItems = new ObservableList<Character>();
            ArsenalItems = new ObservableList<Arsenal>();

            LevelItems.Add(new Level { Name = "Palace" });
            LevelItems.Add(new Level { Name = "Highway" });
            LevelItems.Add(new Level { Name = "Sein" });

            CharacterItems.Add(new Character { Name = "Huey" });
            CharacterItems.Add(new Character { Name = "Dewey" });
            CharacterItems.Add(new Character { Name = "Louie" });

            ArsenalItems.Add(new Arsenal { Name = "Alpha" });
            ArsenalItems.Add(new Arsenal { Name = "Beta" });
            ArsenalItems.Add(new Arsenal { Name = "Cappa" });

            SearchGameType.ComboBoxList.SelectItem(0);
            CreateGameType.ComboBoxList.SelectItem(0);
        }

        public void NavigateToProfileSelect()
        {
            // TODO: Get profiles from db.
            ProfileItems.Clear();
            ProfileItems.Add(new Profile { Name = "Foo" });
            ProfileItems.Add(new Profile { Name = "Bar" });
            ProfileItems.Add(new Profile { Name = "Baz" });
            ProfileItems.Add(new Profile { Name = "Qux" });

            SwitchTo("ProfileSelect");
        }

        public void ProfileClick()
        {
            Session.Profile = ProfileItems.SelectedItem;
            SwitchTo("ProfileHome");
        }

        public void MultiplayerChoice(View view)
        {
            Session.IsHost = view.Data == "CreateGame" ? true : false;
            SwitchTo(view.Data);
        }

        public void Search()
        {
            // TODO: Get lobbies from networking api
            LobbyItems.Clear();
            LobbyItems.Add(new Lobby { Name = "Chip" });
            LobbyItems.Add(new Lobby { Name = "Jojo" });
            LobbyItems.Add(new Lobby { Name = "Gains" });

            SwitchTo("GameLobbies");
        }

        public void LobbyClick()
        {
            Session.Lobby = LobbyItems.SelectedItem;
            SwitchTo("CharacterSelect");
        }

        public void SearchGameTypeSelected(ItemSelectionActionData actionData)
        {
            searchGameType = actionData.ItemView.Text.Value;
        }

        public void CreateGameTypeSelected(ItemSelectionActionData actionData)
        {
            createGameType = actionData.ItemView.Text.Value;
        }

        public void LevelClick()
        {
            Session.Level = LevelItems.SelectedItem;
            SwitchTo("CharacterSelect");
        }

        public void CharacterClick()
        {
            Session.Character = CharacterItems.SelectedItem;
            SwitchTo("ArsenalSelect");
        }

        public void ArsenalClick()
        {
            Session.Arsenal = ArsenalItems.SelectedItem;
            SceneManager.LoadScene(sceneName: "GameLobby");
        }

        public void Quit()
        {
            Debug.Log("Quit() called.");
        }

        public void NavigateTo(View view)
        {
            SwitchTo(view.Data);
        }

        public void Back()
        {
            ViewSwitcherHistory.Pop();
            ContentViewSwitcher.SwitchTo(ViewSwitcherHistory.Peek());
        }

        private void SwitchTo(string id)
        {
            ViewSwitcherHistory.Push(id);
            ContentViewSwitcher.SwitchTo(id);
        }
    }

    public class _Profile : ViewField<Profile> { public static implicit operator Profile(_Profile value) { return value.Value; } }
}
