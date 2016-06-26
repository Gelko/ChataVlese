var gulp = require('gulp'),
    print = require('gulp-print'),
    watch = require('gulp-watch'),
    argv = require('yargs').argv,
    fs = require('fs'),
    path = require('path'),
    changed = require('gulp-changed'),
    Installer = require('dep-copy'),
    Transformer = require('xdt-transform'),
    exec = require('child_process').execSync;

/*
 * PROPERTIES INITIALIZATION
 */
var deployPath = 'C:/_dev/sunflower_dev/';
var filesToBeWatched = ['App/**/*.*', 'Configuration/**/*.json'];
var packageName = "Sunflower";
var basePath = '.';
var filesToBeInstalled = [
	'App/**/*.*',
	'Configuration/**/*.json',
	'bin/**/*' + packageName + '*.dll'
];

if (argv.deployPath) {
    deployPath = argv.deployPath;
}

gulp.task('copy', function () {
    gulp.src(filesToBeWatched, { base: '.' })
			.pipe(changed(deployPath))
			.pipe(print())
			.pipe(gulp.dest(deployPath));
});

gulp.task('watch', function () {
    gulp.watch(filesToBeWatched, ['copy']);
});

gulp.task('install', ['transform'], function () {
    var installer = new Installer(basePath, packageName, deployPath);

    gulp.src(filesToBeInstalled, { base: basePath })
		.pipe(installer.copyFiles())
		.pipe(installer.addToIndexFile());
});

gulp.task('transform', function () {
    if (!argv.conf)
        argv.conf = 'Debug';

    var source = path.join(deployPath, 'Web.config');
    var transform = 'Web.##.config'.replace("##", argv.conf);
    var destination = source;

    var transformer = new Transformer();
    transformer.transform(source, transform, destination, null, function () {
        source = path.join(deployPath, 'Web.config');
    });
});