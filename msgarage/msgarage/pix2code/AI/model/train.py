#!/usr/bin/env python
from __future__ import print_function
from __future__ import absolute_import
__author__ = 'Tony Beltramelli - www.tonybeltramelli.com'

import tensorflow as tf
sess = tf.Session(config=tf.ConfigProto(log_device_placement=True))

import sys

from classes.dataset.Generator import *
from classes.model.pix2code import *


def run(input_path1,input_path2, output_path, platform,is_memory_intensive=False, pretrained_model=None):
    np.random.seed(1234)

    train_dataset = Dataset()
    train_dataset.load(input_path1, generate_binary_sequences=True)
    train_dataset.save_metadata(output_path)
    train_dataset.voc.save(output_path)

    val_dataset = Dataset()
    val_dataset.load(input_path2, generate_binary_sequences=True)
    #val_dataset.save_metadata(output_path)
    #val_dataset.voc.save(output_path)

    if not is_memory_intensive:
        dataset.convert_arrays()
        input_shape = dataset.input_shape
        output_size = dataset.output_size

        print(len(dataset.input_images), len(dataset.partial_sequences), len(dataset.next_words))
        print(dataset.input_images.shape, dataset.partial_sequences.shape, dataset.next_words.shape)
    else:
        train_gui_paths, train_img_paths = Dataset.load_paths_only(input_path1)

        val_gui_paths, val_img_paths = Dataset.load_paths_only(input_path2)


        input_shape = train_dataset.input_shape
        output_size = train_dataset.output_size
        train_steps_per_epoch = train_dataset.size / BATCH_SIZE

        val_steps_per_epoch = val_dataset.size / BATCH_SIZE

        voc = Vocabulary()
        voc.retrieve(output_path)

        train_generator = Generator.data_generator(voc, train_gui_paths, train_img_paths, batch_size=BATCH_SIZE, generate_binary_sequences=True)
        val_generator = Generator.data_generator(voc, val_gui_paths, val_img_paths, batch_size=BATCH_SIZE, generate_binary_sequences=True)


    model = pix2code(input_shape, output_size, output_path,voc.size,platform)

    if pretrained_model is not None:
        model.model.load_weights(pretrained_model)

    if not is_memory_intensive:
        model.fit(train_dataset.input_images, train_dataset.partial_sequences, train_dataset.next_words)
    else:
        model.fit_generator(train_generator, steps_per_epoch=train_steps_per_epoch, val_generator=val_generator, val_steps_per_epoch=val_steps_per_epoch)

if __name__ == "__main__":
    argv = sys.argv[1:]

    if len(argv) < 2:
        print("Error: not enough argument supplied:")
        print("train.py <input path> <output path> <is memory intensive (default: 0)> <pretrained weights (optional)>")
        exit(0)
    else:
        input_path1 = argv[0]
        input_path2=argv[1]
        output_path = argv[2]
        platform= argv[3]
        use_generator = False if len(argv) < 5 else True if int(argv[4]) == 1 else False
        pretrained_weigths = None if len(argv) < 6 else argv[5]

    run(input_path1,input_path2, output_path,platform, is_memory_intensive=use_generator, pretrained_model=pretrained_weigths)
