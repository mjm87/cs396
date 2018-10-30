public interface Conversable {
	void ListenTo(string message, Conversable speaker, Conversation conversation);
	string GetName();
}