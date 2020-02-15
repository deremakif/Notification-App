import 'package:flutter/material.dart';

@immutable
class Message {
  final String title;
  final String body;
  final String data1;
  final String data2;

  const Message({
    @required this.title,
    @required this.body,
    @required this.data1,
    @required this.data2,
  });
}
