using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;


namespace GG2
{
   public class ApplicationViewModel : INotifyPropertyChanged
    {
        bool initialized = false;   // была ли начальная инициализация
        Note selectedNote;  // выбранный друг
        private bool isBusy;    // идет ли загрузка с сервера

        public ObservableCollection<Note> Notes { get; set; }
        NoteService notesService = new NoteService();
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CreateNoteCommand { protected set; get; }
        public ICommand DeleteNoteCommand { protected set; get; }
        public ICommand SaveNoteCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }

        public INavigation Navigation { get; set; }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged("IsBusy");
                OnPropertyChanged("IsLoaded");
            }
        }
        public bool IsLoaded
        {
            get { return !isBusy; }
        }

        public ApplicationViewModel()
        {
            Notes = new ObservableCollection<Note>();
            IsBusy = false;
            CreateNoteCommand = new Command(CreateNote);
            DeleteNoteCommand = new Command(DeleteNote);
            SaveNoteCommand = new Command(SaveNote);
            BackCommand = new Command(Back);
        }

        public Note SelectedNote
        {
            get { return selectedNote; }
            set
            {
                if (selectedNote != value)
                {
                    Note tempNote = new Note()
                    {
                        Id = value.Id,
                        Нeader = value.Нeader,
                        Body = value.Body,
                        
                    };
                    selectedNote = null;
                    OnPropertyChanged("SelectedNote");
                    Navigation.PushAsync(new NotePage(tempNote, this));
                }
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private async void CreateNote()
        {
            await Navigation.PushAsync(new NotePage(new Note(), this));
        }
        private void Back()
        {
            Navigation.PopAsync();
        }

        public async Task GetNotes()
        {
            if (initialized == true) return;
            IsBusy = true;
            IEnumerable<Note> notes = await notesService.Get();

            // очищаем список
            
            while (Notes.Any())
                Notes.RemoveAt(Notes.Count - 1);

            // добавляем загруженные данные
            foreach (Note f in notes)
                Notes.Add(f);
            IsBusy = false;
            initialized = true;
        }
        private async void SaveNote(object noteObject)
        {
            Note note = noteObject as Note;
            if (note != null)
            {
                IsBusy = true;
                // редактирование
                if (note.Id > 0)
                {
                    Note updatedNote = await notesService.Update(note);
                    // заменяем объект в списке на новый
                    if (updatedNote != null)
                    {
                        int pos = Notes.IndexOf(updatedNote);
                        Notes.RemoveAt(pos);
                        Notes.Insert(pos, updatedNote);
                    }
                }
                // добавление
                else
                {
                    Note addedNote = await notesService.Add(note);
                    if (addedNote != null)
                        Notes.Add(addedNote);
                }
                IsBusy = false;
            }
            Back();
        }
        private async void DeleteNote(object noteObject)
        {
            Note note = noteObject as Note;
            if (note != null)
            {
                IsBusy = true;
                Note deletedNote = await notesService.Delete(note.Id);
                if (deletedNote != null)
                {
                    Notes.Remove(deletedNote);
                }
                IsBusy = false;
            }
            Back();
        }
    }
}
