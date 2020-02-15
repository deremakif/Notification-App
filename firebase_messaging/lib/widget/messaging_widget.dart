import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:flutter/material.dart';
import 'package:march_1_firebase_messaging/model/message.dart';

class MessagingWidget extends StatefulWidget {
  @override
  _MessagingWidgetState createState() => _MessagingWidgetState();
}

class _MessagingWidgetState extends State<MessagingWidget> {
  final FirebaseMessaging firebaseMessaging = FirebaseMessaging();
  List<Message> messages = [];
  dynamic dMessage;

  @override
  void initState() {
    super.initState();

    firebaseMessaging.configure(
      onMessage: (Map<String, dynamic> message) async {
        print("onMessage: $message");
        final notification = message['notification'];
        final data = message['data'];
        setState(() {
          dMessage = "onMessage: " + message.toString();
          messages.add(
            Message(
              title: notification['title'],
              body: notification['body'],
              data1: data['key1'],
              data2: data['key2'],
            ),
          );
        });
      },
      onLaunch: (Map<String, dynamic> message) async {
        print("onLaunch: $message");

        final notification = message['notification'];
        final data = message['data'];

        setState(() {
          dMessage = "onLaunch: " + message.toString();
          messages.add(
            Message(
              title: '${notification['title']}',
              body: '${notification['body']}',
              data1: data['key1'],
              data2: data['key2'],
            ),
          );
        });
      },
      onResume: (Map<String, dynamic> message) async {
        print("onResume: $message");
        final notification = message['notification'];
        final data = message['data'];
        setState(() {
          dMessage = "onResume: " + message.toString();
          messages.add(
            Message(
              title: '${notification['title']}',
              body: '${notification['body']}',
              data1: data['key1'],
              data2: data['key2'],
            ),
          );
        });
      },
    );
    firebaseMessaging.requestNotificationPermissions(
        const IosNotificationSettings(sound: true, badge: true, alert: true));

    printToken();
  }

  printToken() async {
    String token = await firebaseMessaging.getToken();
    print(token);
  }

  @override
  Widget build(BuildContext context) {
    print(dMessage);
    return ListView(
      children: messages.map(buildMessage).toList(),
    );
  }

  Widget buildMessage(Message message) => ListTile(
        title: Text(message.title + message.body + message.data1 + message.data2),
        subtitle: Text(dMessage.toString()),
        leading: Text(message.data1 ?? "boş"),
      );
}
