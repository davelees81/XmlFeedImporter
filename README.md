# XmlFeedImporter
Multi-threaded import of xml fies into a database

Current progress.

Core functionality is in place, with the import system able to process the CI test files into your existing database. I do expect refinements will be necessary in order to finalise. Having reviewed the DDEX documents, adding these in very much relies on seeing the CI data.

Rough outline of whats outstanding.

Database
Extend Insert_CixImport_Release proc to check for existence of artist / title / album when processing a release.
Potentially split out into more specific procs
Potentially require new tables for website to consume, or possibly an intermediate data migration step (TBC)

Software
Add in error handling / logging mechanism
Change processing model so parse is immediately followed by DB call (as opposed to batches)
Implement additional parsing from initial CI drop analysis (including Shaun Yule checklist)
Implement additional update parsing (can possibly be done at a later date)
Apply file path changes from config

Analysis
Initial CI drop parsing
Work out subsequent update parsing
Review database setup
DDEX documentation
Website access to mp3's

Configuration
Setup FTP for incoming CI data
Configure file paths for xml parsing
Configure mp3 paths for website
Schedule importer as job / or invoke?
