require 'build_utilities.rb'
require 'local_properties.rb'
require 'project_name.rb'
require 'rake/clean'
require 'fileutils'

#load settings that differ by machine
local_settings = LocalSettings.new

COMPILE_TARGET = 'debug'

CLEAN.include('artifacts','**/bin','**/obj')

#target folders that can be run from VS
project_test_dir  = File.join('product',"test.#{Project.name}",'bin','debug')

output_folders = [project_test_dir]

task :default => [:full_test]

task :init  => :clean do
  mkdir 'artifacts'
  mkdir 'artifacts/coverage'
  mkdir 'artifacts/deploy'
end

desc 'compiles the project'
task :compile => :init do
  MSBuildRunner.compile :compile_target => COMPILE_TARGET, :solution_file => 'solution.sln'
end

task :deploy => :compile do
  Dir.glob(File.join('product','**','developwithpassion*.dll')).each do|file|
    FileUtils.cp file,File.join('artifacts','deploy')
  end
end

desc 'run the tests for the project'
task :test, :category_to_exclude, :needs => [:compile] do |t,args|
  args.with_defaults(:category_to_exclude => 'SLOW')
  runner = MbUnitRunner.new :compile_target => COMPILE_TARGET, :category_to_exclude => args.category_to_exclude
  runner.execute_tests ["test.#{Project.name}"]
end

desc 'run the bdddoc test report for the project'
task :run_test_report => [:test] do
 runner = BDDDocRunner.new 
 runner.run(File.join('product','test.developwithpassion.bdd','bin','debug','test.developwithpassion.bdd.dll'))
end

